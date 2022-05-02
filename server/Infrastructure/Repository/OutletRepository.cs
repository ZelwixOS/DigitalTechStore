namespace Infrastructure.Repository
{
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class OutletRepository : BaseRepository, IOutletRepository
    {
        public OutletRepository(string connectionString, IDatabaseContextFactory contextFactory)
    : base(connectionString, contextFactory)
        {
        }

        public Outlet CreateItem(Outlet item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<Outlet> GetItems()
        {
            return this.Context.Outlets.Include(o => o.City).ThenInclude(c => c.Region).AsNoTracking();
        }

        public Outlet GetItem(int id)
        {
            return this.Context.Outlets.Include(o => o.City).ThenInclude(c => c.Region).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(Outlet item)
        {
            this.Context.Outlets.Remove(item);
            return this.Context.SaveChanges();
        }

        public Outlet UpdateItem(Outlet item)
        {
            var entity = this.Context.Outlets.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
