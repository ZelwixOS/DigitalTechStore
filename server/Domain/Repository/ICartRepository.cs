namespace Domain.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;

    public interface ICartRepository : IRepository<Cart, Guid>
    {
        public IQueryable<Cart> GetItemsByUser(Guid userId);
    }
}
