namespace Domain.Models
{
    using System;

    public class Cart
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Count { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
