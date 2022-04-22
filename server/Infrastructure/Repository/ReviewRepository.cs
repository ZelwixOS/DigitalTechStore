namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ReviewRepository : BaseRepository, IReviewRepository, IDisposable
    {
        public ReviewRepository(string connectionString, IDatabaseContextFactory contextFactory)
         : base(connectionString, contextFactory)
        {
            this.Context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public Review CreateItem(Review review)
        {
            var entity = this.Context.Add(review);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public Review GetItem(Guid productId, Guid userId)
        {
            return this.Context.Reviews.AsNoTracking().FirstOrDefault(p => p.ProductId == productId && p.UserId == userId);
        }

        public IQueryable<Review> GetItems()
        {
            return this.Context.Reviews.AsNoTracking();
        }

        public Review GetItem(Guid id)
        {
            return this.Context.Reviews.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(Review review)
        {
            this.Context.Reviews.Remove(review);
            return this.Context.SaveChanges();
        }

        public Review UpdateItem(Review review)
        {
            var entity = this.Context.Reviews.Update(review);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
