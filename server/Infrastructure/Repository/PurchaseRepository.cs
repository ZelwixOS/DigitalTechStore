namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PurchaseRepository : BaseRepository, IPurchaseRepository
    {
        public PurchaseRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public Purchase CreateItem(Purchase item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(Purchase item)
        {
            this.Context.Purchase.Remove(item);
            return this.Context.SaveChanges();
        }

        public Purchase GetItem(Guid id)
        {
            return this.Context.Purchase
                .Include(o => o.Delivery)
                .Include(o => o.DeliveryOutlet)
                .Include(o => o.Outlet)
                .Include(o => o.PurchaseItems)
                .Include(o => o.Seller)
                .Include(o => o.Customer)
                .AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Purchase> GetItems()
        {
            return this.Context.Purchase
                .Include(o => o.PurchaseItems)
                .Include(o => o.Customer)
                .Include(o => o.Seller).AsNoTracking();
        }

        public Purchase UpdateItem(Purchase item)
        {
            var entity = this.Context.Purchase.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
