namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TechParameterRepository : BaseRepository, ITechParameterRepository
    {
        private DatabaseContext context;

        public TechParameterRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public IQueryable<TechParameter> GetItems()
        {
            return context.TechParameters.AsNoTracking();
        }

        public TechParameter GetItem(Guid id)
        {
            return context.TechParameters.Include(i => i.ProductParameters).AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public TechParameter CreateItem(TechParameter techParameter)
        {
            var entity = context.Add(techParameter);
            context.SaveChanges();
            return entity.Entity;
        }

        public TechParameter UpdateItem(TechParameter techParameter)
        {
            var entity = context.TechParameters.Update(techParameter);
            context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(TechParameter techParameter)
        {
            context.TechParameters.Remove(techParameter);
            return context.SaveChanges();
        }
    }
}
