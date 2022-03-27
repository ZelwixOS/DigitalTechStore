namespace Domain.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;

    public interface IWishRepository : IRepository<Wish>
    {
        public IQueryable<Wish> GetItemsByUser(Guid userId);
    }
}
