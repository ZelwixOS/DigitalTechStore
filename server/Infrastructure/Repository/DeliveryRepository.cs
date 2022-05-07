namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DeliveryRepository : BaseRepository, IDeliveryRepository
    {
        public DeliveryRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public Delivery CreateItem(Delivery item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(Delivery item)
        {
            this.Context.Deliveries.Remove(item);
            return this.Context.SaveChanges();
        }

        public Delivery GetItem(Guid id)
        {
            return this.Context.Deliveries.Include(o => o.Purchase).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Delivery> GetItems()
        {
            return this.Context.Deliveries.Include(o => o.Purchase).AsNoTracking();
        }

        public Delivery UpdateItem(Delivery item)
        {
            var entity = this.Context.Deliveries.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
