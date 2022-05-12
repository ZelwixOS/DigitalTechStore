namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TechParameterRepository : BaseRepository, ITechParameterRepository
    {
        public TechParameterRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.Context = this.ContextFactory
                .CreateDbContext(this.ConnectionString);
        }

        public IQueryable<TechParameter> GetItems()
        {
            return this.Context.TechParameters
                .Include(t => t.ParameterBlock)
                .Include(t => t.ParameterValues)
                .AsNoTracking();
        }

        public TechParameter GetItem(Guid id)
        {
            return this.Context.TechParameters
                .Include(i => i.ProductParameters)
                .AsNoTracking()
                .FirstOrDefault(t => t.Id == id);
        }

        public TechParameter CreateItem(TechParameter techParameter)
        {
            var entity = this.Context.Add(techParameter);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public TechParameter UpdateItem(TechParameter techParameter)
        {
            var entity = this.Context.TechParameters
                .Update(techParameter);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(TechParameter techParameter)
        {
            this.Context.TechParameters
                .Remove(techParameter);
            return this.Context.SaveChanges();
        }
    }
}
