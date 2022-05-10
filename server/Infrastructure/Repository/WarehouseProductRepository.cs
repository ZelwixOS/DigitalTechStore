namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class WarehouseProductRepository : BaseRepository, IWarehouseProductRepository
    {
        public WarehouseProductRepository(string connectionString, IDatabaseContextFactory contextFactory)
    : base(connectionString, contextFactory)
        {
        }

        public WarehouseProduct CreateItem(WarehouseProduct item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<WarehouseProduct> GetItems()
        {
            return this.Context.WarehouseProducts
                .Include(wp => wp.Warehouse).ThenInclude(w => w.ReservedProducts).AsNoTracking();
        }

        public WarehouseProduct GetItem(Guid id)
        {
            return this.Context.WarehouseProducts.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(WarehouseProduct item)
        {
            this.Context.WarehouseProducts.Remove(item);
            return this.Context.SaveChanges();
        }

        public WarehouseProduct UpdateItem(WarehouseProduct item)
        {
            var entity = this.Context.WarehouseProducts.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public WarehouseProduct GetItem(Guid productId, int unitId)
        {
            return this.Context.WarehouseProducts.AsNoTracking().FirstOrDefault(p => p.ProductId == productId && p.UnitId == unitId);
        }
    }
}
