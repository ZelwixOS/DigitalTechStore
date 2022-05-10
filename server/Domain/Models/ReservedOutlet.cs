namespace Domain.Models
{
    using System;

    public class ReservedOutlet
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int OutletId { get; set; }

        public Guid PurchaseId { get; set; }

        public int Count { get; set; }

        public Product Product { get; set; }

        public Outlet Outlet { get; set; }

        public Purchase Purchase { get; set; }
    }
}
