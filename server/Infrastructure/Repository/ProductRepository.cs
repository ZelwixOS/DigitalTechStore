namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ProductRepository : BaseRepository, IProductRepository, IDisposable
    {
        public ProductRepository(string connectionString, IDatabaseContextFactory contextFactory)
         : base(connectionString, contextFactory)
        {
        }

        public Product CreateItem(Product product)
        {
            var entity = this.Context.Add(product);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<Product> GetItems()
        {
            return this.Context.Products.Include(p => p.ProductParameters).AsNoTracking();
        }

        public Product GetItem(Guid id)
        {
            return this.Context.Products.Include(p => p.Category).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductWithParameters(Guid id)
        {
            return this.Context.Products
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryParameterBlocks)
                    .ThenInclude(cb => cb.ParameterBlock)
                .Include(p => p.ProductParameters)
                .ThenInclude(pp => pp.TechParameter)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(Product product)
        {
            this.Context.Products.Remove(product);
            return this.Context.SaveChanges();
        }

        public Product UpdateItem(Product product)
        {
            var entity = this.Context.Products.Update(product);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
