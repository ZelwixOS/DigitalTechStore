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

    public class CategoryRepository : BaseRepository, ICategoryRepository, IDisposable
    {
        private readonly DatabaseContext context;
        private bool disposed = false;

        public CategoryRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public Category CreateItem(Category category)
        {
            var entity = context.Add(category);
            context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<Category> GetItems()
        {
            return context.Categories.AsNoTracking();
        }

        public Category GetItem(Guid id)
        {
            return context.Categories.Include(c => c.Products).Include(c => c.Parameters).AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public Category GetItem(string name)
        {
            return context.Categories.Include(c => c.Parameters).AsNoTracking().FirstOrDefault(c => c.Name == name);
        }

        public int DeleteItem(Category category)
        {
            context.Categories.Remove(category);
            return context.SaveChanges();
        }

        public Category UpdateItem(Category category)
        {
            var entity = context.Categories.Update(category);
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
