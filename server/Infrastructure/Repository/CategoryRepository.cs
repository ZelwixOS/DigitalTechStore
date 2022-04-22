namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class CategoryRepository : BaseRepository, ICategoryRepository, IDisposable
    {
        public CategoryRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public Category CreateItem(Category category)
        {
            var entity = this.Context.Add(category);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public IQueryable<Category> GetItems()
        {
            return this.Context.Categories.AsNoTracking();
        }

        public Category GetItem(Guid id)
        {
            var category = this.Context.Categories.Include(c => c.Products).Include(c => c.CategoryParameterBlocks).ThenInclude(p => p.ParameterBlock).ThenInclude(b => b.Parameters).FirstOrDefault(c => c.Id == id);
            category.ParameterBlocks = category.CategoryParameterBlocks.Select(p => p.ParameterBlock).ToHashSet();
            return category;
        }

        public Category GetItem(string name)
        {
            var category = this.Context.Categories.Include(c => c.CategoryParameterBlocks).ThenInclude(p => p.ParameterBlock).ThenInclude(b => b.Parameters).AsNoTracking().FirstOrDefault(c => c.Name == name);
            category.ParameterBlocks = category.CategoryParameterBlocks.Select(p => p.ParameterBlock).ToHashSet();
            return category;
        }

        public int DeleteItem(Category category)
        {
            this.Context.Categories.Remove(category);
            return this.Context.SaveChanges();
        }

        public Category UpdateItem(Category category)
        {
            var entity = this.Context.Categories.Update(category);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
