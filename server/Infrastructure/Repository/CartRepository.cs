namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CartRepository : BaseRepository, ICartRepository, IDisposable
    {
        public CartRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public Cart CreateItem(Cart item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(Cart item)
        {
            this.Context.Carts.Remove(item);
            return this.Context.SaveChanges();
        }

        public int DeleteItems(IQueryable<Cart> items)
        {
            this.Context.Carts.RemoveRange(items);
            return this.Context.SaveChanges();
        }

        public Cart GetItem(Guid id)
        {
            return this.Context.Carts.FirstOrDefault(w => w.Id == id);
        }

        public IQueryable<Cart> GetItemsByUser(Guid userId)
        {
            return this.Context.Carts.Where(w => w.UserId == userId);
        }

        public IQueryable<Cart> GetItems()
        {
            return this.Context.Carts.AsNoTracking();
        }

        public Cart UpdateItem(Cart item)
        {
            var entity = this.Context.Carts.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
