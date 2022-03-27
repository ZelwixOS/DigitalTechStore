namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class WishRepository : BaseRepository, IWishRepository, IDisposable
    {
        private readonly DatabaseContext context;
        private bool disposed = false;

        public WishRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public Wish CreateItem(Wish item)
        {
            var entity = context.Add(item);
            context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(Wish item)
        {
            context.Wishes.Remove(item);
            return context.SaveChanges();
        }

        public Wish GetItem(Guid id)
        {
            return context.Wishes.FirstOrDefault(w => w.Id == id);
        }

        public IQueryable<Wish> GetItemsByUser(Guid userId)
        {
            return context.Wishes.Where(w => w.UserId == userId);
        }

        public IQueryable<Wish> GetItems()
        {
            return context.Wishes.AsNoTracking();
        }

        public Wish UpdateItem(Wish item)
        {
            var entity = context.Wishes.Update(item);
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
