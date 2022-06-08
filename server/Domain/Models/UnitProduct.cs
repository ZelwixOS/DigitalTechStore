namespace Domain.Models
{
    using System;

    public abstract class UnitProduct
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int UnitId { get; set; }

        public int Count { get; set; }

        public Product Product { get; set; }
    }
}
