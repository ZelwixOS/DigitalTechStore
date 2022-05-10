namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ReservedWarehouseRepository : BaseRepository, IReservedWarehouseRepository
    {
        public ReservedWarehouseRepository(string connectionString, IDatabaseContextFactory contextFactory)
    : base(connectionString, contextFactory)
        {
        }

        public ReservedWarehouse CreateItem(ReservedWarehouse item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(ReservedWarehouse item)
        {
            this.Context.WarehousesReserved.Remove(item);
            return this.Context.SaveChanges();
        }

        public int DeleteItems(IQueryable<ReservedWarehouse> items)
        {
            this.Context.WarehousesReserved.RemoveRange(items);
            return this.Context.SaveChanges();
        }

        public ReservedWarehouse GetItem(Guid id)
        {
            return this.Context.WarehousesReserved.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<ReservedWarehouse> GetItems()
        {
            return this.Context.WarehousesReserved.AsNoTracking();
        }

        public ReservedWarehouse UpdateItem(ReservedWarehouse item)
        {
            var entity = this.Context.WarehousesReserved.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
