namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class OutletProductRepository : BaseRepository, IOutletProductRepository
    {
        public OutletProductRepository(string connectionString, IDatabaseContextFactory contextFactory)
    : base(connectionString, contextFactory)
        {
        }

        public OutletProduct CreateItem(OutletProduct item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<OutletProduct> GetItems()
        {
            return this.Context.OutletProducts
                .Include(op => op.Outlet).ThenInclude(o => o.ReservedProducts).AsNoTracking();
        }

        public OutletProduct GetItem(Guid id)
        {
            return this.Context.OutletProducts.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(OutletProduct item)
        {
            this.Context.OutletProducts.Remove(item);
            return this.Context.SaveChanges();
        }

        public OutletProduct UpdateItem(OutletProduct item)
        {
            var entity = this.Context.OutletProducts.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public OutletProduct GetItem(Guid productId, int unitId)
        {
            return this.Context.OutletProducts.AsNoTracking().FirstOrDefault(p => p.ProductId == productId && p.UnitId == unitId);
        }
    }
}
