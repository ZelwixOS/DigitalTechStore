namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CartRepository : BaseRepository, ICartRepository, IDisposable
    {
        private readonly DatabaseContext context;
        private bool disposed = false;

        public CartRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public Cart CreateItem(Cart item)
        {
            var entity = context.Add(item);
            context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(Cart item)
        {
            context.Carts.Remove(item);
            return context.SaveChanges();
        }

        public Cart GetItem(Guid id)
        {
            return context.Carts.FirstOrDefault(w => w.Id == id);
        }

        public IQueryable<Cart> GetItemsByUser(Guid userId)
        {
            return context.Carts.Where(w => w.UserId == userId);
        }

        public IQueryable<Cart> GetItems()
        {
            return context.Carts.AsNoTracking();
        }

        public Cart UpdateItem(Cart item)
        {
            var entity = context.Carts.Update(item);
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
