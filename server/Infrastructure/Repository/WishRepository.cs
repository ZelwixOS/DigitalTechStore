namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class WishRepository : BaseRepository, IWishRepository, IDisposable
    {
        public WishRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public Wish CreateItem(Wish item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(Wish item)
        {
            this.Context.Wishes.Remove(item);
            return this.Context.SaveChanges();
        }

        public Wish GetItem(Guid id)
        {
            return this.Context.Wishes.FirstOrDefault(w => w.Id == id);
        }

        public IQueryable<Wish> GetItemsByUser(Guid userId)
        {
            return this.Context.Wishes.Where(w => w.UserId == userId);
        }

        public IQueryable<Wish> GetItems()
        {
            return this.Context.Wishes.AsNoTracking();
        }

        public Wish UpdateItem(Wish item)
        {
            var entity = this.Context.Wishes.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
