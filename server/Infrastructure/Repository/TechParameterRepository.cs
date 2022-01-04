namespace CleanArchitecture.Infra.Data.Repositories
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class TechParameterRepository : ITechParameterRepository
    {
        private DatabaseContext context;

        public TechParameterRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<TechParameter> GetItems()
        {
            return context.TechParameters.AsNoTracking();
        }

        public TechParameter GetItem(Guid id)
        {
            return context.TechParameters.Include(i => i.ProductParameters).Include(i => i.Category).AsNoTracking().FirstOrDefault(t => t.Id == id);
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
