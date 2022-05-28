namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Response.Statistics;
    using Application.Helpers;
    using Application.Interfaces;

    using Domain.Models;
    using Domain.Repository;

    public class StatisticsService : IStatisticsService
    {
        private readonly IPurchaseRepository purchaseRepository;

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
    }
}
