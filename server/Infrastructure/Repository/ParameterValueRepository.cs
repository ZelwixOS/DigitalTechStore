namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ParameterValueRepository : BaseRepository, IParameterValueRepository
    {
        public ParameterValueRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.Context = this.ContextFactory
                .CreateDbContext(this.ConnectionString);
        }

        public ParameterValue CreateItem(ParameterValue item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(ParameterValue item)
        {
            this.Context.ParameterValues.Remove(item);
            return this.Context.SaveChanges();
        }

        public ParameterValue GetItem(Guid id)
        {
            return this.Context.ParameterValues
                .AsNoTracking()
                .FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<ParameterValue> GetItems()
        {
            return this.Context.ParameterValues
                .AsNoTracking();
        }

        public ParameterValue UpdateItem(ParameterValue item)
        {
            var entity = this.Context.ParameterValues
                .Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
