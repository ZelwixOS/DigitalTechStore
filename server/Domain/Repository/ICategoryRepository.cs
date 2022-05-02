namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        public Category GetItem(string name);
    }
}
