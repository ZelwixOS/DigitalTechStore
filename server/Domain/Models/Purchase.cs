namespace Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class Purchase
    {
        public Guid Id { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? SellerId { get; set; }

        public int? OutletId { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerTelephone { get; set; }

        public PaymentType PaymentType { get; set; }

        public PurchaseStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? DeliveryId { get; set; }

        public int? DeliveryOutletId { get; set; }

        public decimal TotalCost { get; set; }

        public User Customer { get; set; }

        public User Seller { get; set; }

        public Delivery Delivery { get; set; }

        public Outlet Outlet { get; set; }

        public Outlet DeliveryOutlet { get; set; }

        public HashSet<PurchaseItem> PurchaseItems { get; set; }

        public HashSet<ReservedOutlet> OutletsReserved { get; set; }

        public HashSet<ReservedWarehouse> WarehousesReserved { get; set; }
    }
}
