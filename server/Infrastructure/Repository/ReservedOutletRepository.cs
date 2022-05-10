namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ReservedOutletRepository : BaseRepository, IReservedOutletRepository
    {
        public ReservedOutletRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public ReservedOutlet CreateItem(ReservedOutlet item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(ReservedOutlet item)
        {
            this.Context.OutletsReserved.Remove(item);
            return this.Context.SaveChanges();
        }

        public ReservedOutlet GetItem(Guid id)
        {
            return this.Context.OutletsReserved.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<ReservedOutlet> GetItems()
        {
            return this.Context.OutletsReserved.AsNoTracking();
        }

        public ReservedOutlet UpdateItem(ReservedOutlet item)
        {
            var entity = this.Context.OutletsReserved.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
