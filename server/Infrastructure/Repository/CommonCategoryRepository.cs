namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CommonCategoryRepository : BaseRepository, ICommonCategoryRepository, IDisposable
    {
        public CommonCategoryRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.Context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public CommonCategory CreateItem(CommonCategory category)
        {
            var entity = this.Context.Add(category);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<CommonCategory> GetItems()
        {
            return this.Context.CommonCategories.Include(c => c.Categories).AsNoTracking();
        }

        public CommonCategory GetItem(Guid id)
        {
            return this.Context.CommonCategories.Include(c => c.Categories).FirstOrDefault(c => c.Id == id);
        }

        public CommonCategory GetItem(string name)
        {
            return this.Context.CommonCategories.Include(c => c.Categories).AsNoTracking().FirstOrDefault(c => c.Name == name);
        }

        public int DeleteItem(CommonCategory category)
        {
            this.Context.CommonCategories.Remove(category);
            return this.Context.SaveChanges();
        }

        public CommonCategory UpdateItem(CommonCategory category)
        {
            var entity = this.Context.CommonCategories.Update(category);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
