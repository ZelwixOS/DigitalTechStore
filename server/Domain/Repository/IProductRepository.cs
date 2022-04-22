namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface IProductRepository : IRepository<Product>
    {
        public Product GetProductWithParameters(Guid id);
    }
}
