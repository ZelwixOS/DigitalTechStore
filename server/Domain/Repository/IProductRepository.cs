namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface IProductRepository : IRepository<Product, Guid>
    {
        public Product GetProductWithParameters(Guid id);
    }
}
