namespace Domain.Models
{
    using System;

    public class ReservedWarehouse
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int WarehouseId { get; set; }

        public Guid PurchaseId { get; set; }

        public int Count { get; set; }

        public Product Product { get; set; }

        public Warehouse Warehouse { get; set; }

        public Purchase Purchase { get; set; }
    }
}
