namespace Domain.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;

    public interface IProductParameterRepository : IRepository<ProductParameter, Guid>
    {
        IQueryable<ProductParameter> GetItems(Guid id);
    }
}
