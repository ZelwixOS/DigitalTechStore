namespace Infrastructure.Repository
{
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class WarehouseRepository : BaseRepository, IWarehouseRepository
    {
        public WarehouseRepository(string connectionString, IDatabaseContextFactory contextFactory)
    : base(connectionString, contextFactory)
        {
        }

        public Warehouse CreateItem(Warehouse item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<Warehouse> GetItems()
        {
            return this.Context.Warehouses.Include(c => c.City).ThenInclude(c => c.Region).AsNoTracking();
        }

        public Warehouse GetItem(int id)
        {
            return this.Context.Warehouses.Include(c => c.City).ThenInclude(c => c.Region).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(Warehouse item)
        {
            this.Context.Warehouses.Remove(item);
            return this.Context.SaveChanges();
        }

        public Warehouse UpdateItem(Warehouse item)
        {
            var entity = this.Context.Warehouses.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
