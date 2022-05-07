namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PurchaseItemRepository : BaseRepository, IPurchaseItemRepository
    {
        public PurchaseItemRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public PurchaseItem CreateItem(PurchaseItem item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(PurchaseItem item)
        {
            this.Context.PurchaseItems.Remove(item);
            return this.Context.SaveChanges();
        }

        public PurchaseItem GetItem(Guid id)
        {
            return this.Context.PurchaseItems.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<PurchaseItem> GetItems()
        {
            return this.Context.PurchaseItems.AsNoTracking();
        }

        public PurchaseItem UpdateItem(PurchaseItem item)
        {
            var entity = this.Context.PurchaseItems.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
