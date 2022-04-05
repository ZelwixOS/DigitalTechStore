namespace Domain.Models
{
    using System;

    public class Wish
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
