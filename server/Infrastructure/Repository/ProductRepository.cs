namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ProductRepository : BaseRepository, IProductRepository, IDisposable
    {
        private readonly DatabaseContext context;
        private bool disposed = false;

        public ProductRepository(string connectionString, IDatabaseContextFactory contextFactory)
         : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public Product CreateItem(Product product)
        {
            var entity = context.Add(product);
            context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<Product> GetItems()
        {
            return context.Products.Include(p => p.ProductParameters).AsNoTracking();
        }

        public Product GetItem(Guid id)
        {
            return context.Products.Include(p => p.Category).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(Product product)
        {
            context.Products.Remove(product);
            return context.SaveChanges();
        }

        public Product UpdateItem(Product product)
        {
            var entity = context.Products.Update(product);
            context.SaveChanges();
            return entity.Entity;
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}
