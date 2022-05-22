namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class ProductDto : IComparable
    {
        public ProductDto(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Description = product.Description;
            this.Mark = product.Mark;
            this.VendorCode = product.VendorCode;
            this.PicURL = product.PicURL;
            this.PriceWithoutDiscount = product.PriceWithoutDiscount;
            this.Published = product.Published;

            if (product.Category == null)
            {
                this.Category = null;
            }
            else
            {
                this.Category = new CategoryOfProductDto(product.Category);
            }
        }

        public ProductDto(Product product, int cityId, int regionId)
            : this(product)
        {
            if (product.OutletProducts != null && product.OutletsReserved != null)
            {
                    this.OutletProducts = product.OutletProducts
                        .Where(o => o.Outlet != null && o.Outlet.CityId == cityId && o.Count - product.OutletsReserved.Where(r => r.OutletId == o.UnitId).Sum(r => r.Count) > 0)
                        .Select(o => new OutletProductDto(o)).ToList();
            }

            if (product.WarehouseProducts != null && product.WarehousesReserved != null)
            {
                this.IsInWarehouse = product.WarehouseProducts
                    .Any(o => o.Warehouse != null && o.Warehouse.City != null && o.Warehouse.City.RegionId == regionId && o.Count - product.WarehousesReserved.Where(r => r.WarehouseId == o.UnitId).Sum(r => r.Count) > 0);
            }
        }

        public ProductDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? PriceWithoutDiscount { get; set; }

        public string Description { get; set; }

        public double Mark { get; set; }

        public string VendorCode { get; set; }

        public string PicURL { get; set; }

        public List<ParameterOfProductDto> ProductParameter { get; set; }

        public CategoryOfProductDto Category { get; set; }

        public bool InCart { get; set; }

        public bool InWishlist { get; set; }

        public bool Reviewed { get; set; }

        public List<OutletProductDto> OutletProducts { get; set; }

        public bool IsInWarehouse { get; set; }

        public bool Published { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is ProductDto expected)
            {
                if (this.Name == expected.Name && ((this.Category == null && expected.Category == null) || this.Category.CompareTo(expected.Category) == 0) && expected.Price == this.Price && expected.Description == this.Description && expected.Mark == this.Mark && expected.PicURL == this.PicURL && expected.VendorCode == this.VendorCode)
                {
                    if (this.Id == expected.Id)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }

            return -1;
        }
    }
}
