namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? PriceWithoutDiscount { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public double Mark { get; set; }

        [Required]
        [MaxLength(10)]
        public string VendorCode { get; set; }

        [Required]
        public int Popularity { get; set; }

        [Required]
        [MaxLength(100)]
        public string PicURL { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Published { get; set; }

        [Required]
        public Guid CategoryIdFk { get; set; }

        public Category Category { get; set; }

        public HashSet<ProductParameter> ProductParameters { get; set; }

        public HashSet<Cart> CartItems { get; set; }

        public HashSet<Wish> WishedItems { get; set; }

        public HashSet<Review> Reviews { get; set; }

        public HashSet<OutletProduct> OutletProducts { get; set; }

        public HashSet<WarehouseProduct> WarehouseProducts { get; set; }

        public HashSet<PurchaseItem> PurchaseItems { get; set; }

        public HashSet<ReservedWarehouse> WarehousesReserved { get; set; }

        public HashSet<ReservedOutlet> OutletsReserved { get; set; }
    }
}
