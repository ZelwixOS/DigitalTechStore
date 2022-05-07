namespace Domain.Models
{
    using System;

    public class PurchaseItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid PurchaseId { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { get; set; }

        public Product Product { get; set; }

        public Purchase Purchase { get; set; }
    }
}
