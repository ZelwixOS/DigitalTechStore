namespace Domain.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;

    public interface IUserRepository
    {
        public IQueryable<User> GetItems();

        public User GetItem(Guid id);
    }
}
