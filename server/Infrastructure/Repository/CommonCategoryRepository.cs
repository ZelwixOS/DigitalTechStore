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

    public class CommonCategoryRepository : BaseRepository, ICommonCategoryRepository, IDisposable
    {
        private readonly DatabaseContext context;
        private bool disposed = false;

        public CommonCategoryRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public CommonCategory CreateItem(CommonCategory category)
        {
            var entity = context.Add(category);
            context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<CommonCategory> GetItems()
        {
            return context.CommonCategories.Include(c => c.Categories).AsNoTracking();
        }

        public CommonCategory GetItem(Guid id)
        {
            return context.CommonCategories.Include(c => c.Categories).FirstOrDefault(c => c.Id == id);
        }

        public CommonCategory GetItem(string name)
        {
            return context.CommonCategories.Include(c => c.Categories).AsNoTracking().FirstOrDefault(c => c.Name == name);
        }

        public int DeleteItem(CommonCategory category)
        {
            context.CommonCategories.Remove(category);
            return context.SaveChanges();
        }

        public CommonCategory UpdateItem(CommonCategory category)
        {
            var entity = context.CommonCategories.Update(category);
            context.SaveChanges();
            return entity.Entity;
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}
