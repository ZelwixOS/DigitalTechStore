namespace Infrastructure.Repository
{
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(string connectionString, IDatabaseContextFactory contextFactory)
    : base(connectionString, contextFactory)
        {
        }

        public City CreateItem(City item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<City> GetItems()
        {
            return this.Context.Cities.AsNoTracking();
        }

        public City GetItem(int id)
        {
            return this.Context.Cities.Include(c => c.Region).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public int DeleteItem(City item)
        {
            this.Context.Cities.Remove(item);
            return this.Context.SaveChanges();
        }

        public City UpdateItem(City item)
        {
            var entity = this.Context.Cities.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
