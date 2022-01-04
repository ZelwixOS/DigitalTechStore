namespace CleanArchitecture.Infra.Data.Repositories
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Infrastructure.Repository;
    using Microsoft.EntityFrameworkCore;

    public class ProductParameterRepository : BaseRepository, IProductParameterRepository
    {
        private DatabaseContext context;

        public ProductParameterRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public IQueryable<ProductParameter> GetItems()
        {
            return context.ProductParameters.AsNoTracking();
        }

        public IQueryable<ProductParameter> GetItems(Guid id)
        {
            return context.ProductParameters.Where(p => p.Product.Id == id).Include(p => p.Parameter).AsNoTracking();
        }

        public ProductParameter GetItem(Guid id)
        {
            return context.ProductParameters.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public ProductParameter CreateItem(ProductParameter productParameter)
        {
            var entity = context.Add(productParameter);
            context.SaveChanges();
            return entity.Entity;
        }

        public ProductParameter UpdateItem(ProductParameter productParameter)
        {
            var entity = context.ProductParameters.Update(productParameter);
            context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(ProductParameter productParameter)
        {
            context.ProductParameters.Remove(productParameter);
            return context.SaveChanges();
        }
    }
}
