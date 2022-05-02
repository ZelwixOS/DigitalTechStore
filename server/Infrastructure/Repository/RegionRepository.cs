namespace Infrastructure.Repository
{
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class RegionRepository : BaseRepository, IRegionRepository
    {
        public RegionRepository(string connectionString, IDatabaseContextFactory contextFactory)
    : base(connectionString, contextFactory)
        {
        }

        public Region CreateItem(Region item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<Region> GetItems()
        {
            return this.Context.Regions.Include(i => i.Cities).ThenInclude(c => c.Outlets).AsNoTracking();
        }

        public Region GetItem(int id)
        {
            return this.Context.Regions.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(Region item)
        {
            this.Context.Regions.Remove(item);
            return this.Context.SaveChanges();
        }

        public Region UpdateItem(Region item)
        {
            var entity = this.Context.Regions.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
