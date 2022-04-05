namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ReviewRepository : BaseRepository, IReviewRepository, IDisposable
    {
        private readonly DatabaseContext context;
        private bool disposed = false;

        public ReviewRepository(string connectionString, IDatabaseContextFactory contextFactory)
         : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public Review CreateItem(Review review)
        {
            var entity = context.Add(review);
            context.SaveChanges();
            return entity.Entity;
        }

        public Review GetItem(Guid productId, Guid userId)
        {
            return context.Reviews.AsNoTracking().FirstOrDefault(p => p.ProductId == productId && p.UserId == userId);
        }

        public IQueryable<Review> GetItems()
        {
            return context.Reviews.AsNoTracking();
        }

        public Review GetItem(Guid id)
        {
            return context.Reviews.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(Review review)
        {
            context.Reviews.Remove(review);
            return context.SaveChanges();
        }

        public Review UpdateItem(Review review)
        {
            var entity = context.Reviews.Update(review);
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
