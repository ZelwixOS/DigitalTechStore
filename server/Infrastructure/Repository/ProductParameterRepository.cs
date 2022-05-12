namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ProductParameterRepository : BaseRepository, IProductParameterRepository
    {
        public ProductParameterRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public IQueryable<ProductParameter> GetItems()
        {
            return this.Context.ProductParameters
                .Include(pp => pp.ParameterValue)
                .Include(pp => pp.Product)
                .Include(pp => pp.TechParameter)
                .AsNoTracking();
        }

        public IQueryable<ProductParameter> GetItems(Guid id)
        {
            return this.Context.ProductParameters.Where(p => p.Product.Id == id).Include(p => p.TechParameter).AsNoTracking();
        }

        public ProductParameter GetItem(Guid id)
        {
            return this.Context.ProductParameters.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public ProductParameter CreateItem(ProductParameter productParameter)
        {
            var entity = this.Context.Add(productParameter);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public ProductParameter UpdateItem(ProductParameter productParameter)
        {
            var entity = this.Context.ProductParameters.Update(productParameter);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(ProductParameter productParameter)
        {
            this.Context.ProductParameters.Remove(productParameter);
            return this.Context.SaveChanges();
        }
    }
}
