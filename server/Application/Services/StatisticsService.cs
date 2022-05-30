namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using Application.DTO.Response.Statistics;
    using Application.Helpers;
    using Application.Interfaces;

    using Domain.Models;
    using Domain.Repository;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class StatisticsService : IStatisticsService
    {
        private readonly IPurchaseRepository purchaseRepository;

        private readonly string directory = ".\\Docs\\reports\\";

        public StatisticsService(IPurchaseRepository purchaseRepository)
        {
            this.purchaseRepository = purchaseRepository;
        }

        public List<SalesTimeStatistics> GetOrdersForMonth(User user, StatisticsType type, bool finished)
        {
            DateTime to = DateTime.Today.AddDays(1);
            DateTime from = to.AddDays(-31);

            return this.GetOrdersForPeriod(from, to, user.OutletId, type, false);
        }

        public SalesStatisticsForPeriod GetSalesForMonth(User user, StatisticsType type)
        {
            DateTime to = DateTime.Today.AddDays(1);
            DateTime from = to.AddDays(-31);
            var inPeriod = this.purchaseRepository.GetItems().Where(p => p.CreatedDate > from && p.CreatedDate < to);
            var stats = this.GetSales(inPeriod, user.OutletId, type);

            return stats;
        }

        public SalesStatisticsForPeriod GetSalesTotal(User user, StatisticsType type)
        {
            var total = this.purchaseRepository.GetItems();
            var stats = this.GetSales(total, user.OutletId, type);

            return stats;
        }

        public string GetStatFile(User user, string fromStr, string toStr)
        {
            if (fromStr == null || toStr == null)
            {
                return null;
            }

            DateTime to;
            DateTime from;

            if (!DateTime.TryParse(fromStr, out from) || !DateTime.TryParse(toStr, out to))
            {
                return null;
            }

            var name = $"{directory}statistics_outlet_{user.OutletId}_from_{from.ToShortDateString()}_to_{to.ToShortDateString()}.xlsx";

            var items = this.purchaseRepository
                .GetItems()
                .Where(p => p.CreatedDate > from && p.CreatedDate < to && p.OutletId == user.OutletId && p.Status == PurchaseStatus.Finished)
                .SelectMany(p => p.PurchaseItems);

            var grouppedItems = items.GroupBy(p => new { p.ProductId, ProductName = p.Product.Name, Code = p.Product.VendorCode, Category = p.Product.Category.Name });

            var orderedPreItems = grouppedItems
                .Select(p => new StatClass()
                {
                    ProductName = p.Key.ProductName,
                    ProductId = p.Key.ProductId,
                    Category = p.Key.Category,
                    Count = p.Select(r => r.Count).Sum(),
                    Sum = p.Select(r => r.Sum).Sum(),
                    Code = p.Key.Code,
                })
                .OrderBy(p => p.Sum)
                .ToList();

            CreateExcel(name, orderedPreItems);

            return name;
        }

        protected SalesStatisticsForPeriod GetSales(IQueryable<Purchase> purchase, int? outletId, StatisticsType type)
        {
            var purch = purchase.Where(p => p.OutletId == outletId);
            var getter = this.DataGetter(type);
            var stats = new SalesStatisticsForPeriod();
            stats.NotCompleted = getter(purch.Where(p => p.Status == PurchaseStatus.InDelivering ||
                p.Status == PurchaseStatus.TransportingToOutlet ||
                p.Status == PurchaseStatus.WaitsInOulet ||
                p.Status == PurchaseStatus.WaitsForPaymentAprroval ||
                p.Status == PurchaseStatus.WaitsForDelivery));
            stats.Finished = getter(purch.Where(p => p.Status == PurchaseStatus.Finished));
            stats.Canceled = getter(purch.Where(p => p.Status == PurchaseStatus.CanceledByClient));
            stats.Refused = getter(purch.Where(p => p.Status == PurchaseStatus.Refused));

            return stats;
        }

        protected List<SalesTimeStatistics> GetOrdersForPeriod(DateTime from, DateTime to, int? outletId, StatisticsType type, bool finished)
        {
            var salesOfPeriodFromOutlet = this.purchaseRepository.GetItems().Where(p => p.CreatedDate > from && p.CreatedDate < to && p.OutletId == outletId && (!finished || p.Status == PurchaseStatus.Finished));

            var getter = this.DataGetter(type);

            var stats = new List<SalesTimeStatistics>();
            for (DateTime day = from; day < to; day = day.AddDays(1))
            {
                stats.Add(new SalesTimeStatistics() { Date = day, Data = getter(salesOfPeriodFromOutlet.Where(p => p.CreatedDate.Date == day.Date)) });
            }

            return stats;
        }

        protected Func<IQueryable<Purchase>, double> DataGetter(StatisticsType type)
        {
            Func<IQueryable<Purchase>, double> getter = (IQueryable<Purchase> pur) => pur.Count();

            switch (type)
            {
                case StatisticsType.Orders:
                    getter = (IQueryable<Purchase> pur) => pur.Count();
                    break;
                case StatisticsType.Items:
                    getter = (IQueryable<Purchase> pur) => pur.Select(p => p.PurchaseItems.Count() > 0 ? p.PurchaseItems.Sum(pi => pi.Count) : 0).ToList().Sum();
                    break;
                case StatisticsType.Money:
                    getter = (IQueryable<Purchase> pur) => decimal.ToDouble(pur.Select(p => p.PurchaseItems.Count() > 0 ? p.PurchaseItems.Sum(pi => pi.Sum) : 0).ToList().Sum());
                    break;
                default:
                    getter = (IQueryable<Purchase> pur) => pur.Count();
                    break;
            }

            return getter;
        }

        protected void CreateExcel(string name, List<StatClass> items)
        {
            if (File.Exists(name))
            {
                return;
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            else
            {
                if (Directory.GetFiles(directory).Length > 500)
                {
                    Directory.Delete(directory, true);
                }
            }

            using (var fs = new FileStream(name, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Статистика за период");

                List<string> columns = new List<string>()
                {
                    "Продукт",
                    "Категория",
                    "Продано за период (шт.)",
                    "Общая выручка (руб.)",
                    "Артикул",
                };

                IRow row = excelSheet.CreateRow(0);
                int columnIndex = 0;

                foreach (string column in columns)
                {
                    row.CreateCell(columnIndex).SetCellValue(column);
                    columnIndex++;
                }

                int rowIndex = 1;
                ICell cell;
                foreach (var item in items)
                {
                    row = excelSheet.CreateRow(rowIndex);

                    for (int i = 0; i < 5; i++)
                    {
                        cell = row.CreateCell(i);
                        switch (i)
                        {
                            case 0:
                                cell.SetCellValue(item.ProductName);
                                break;
                            case 1:
                                cell.SetCellValue(item.Category);
                                break;
                            case 2:
                                cell.SetCellValue(item.Count);
                                break;
                            case 3:
                                cell.SetCellValue(decimal.ToDouble(item.Sum));
                                break;
                            case 4:
                                cell.SetCellValue(item.Code);
                                break;
                        }
                    }

                    rowIndex++;
                }

                workbook.Write(fs);
            }
        }
    }
}
