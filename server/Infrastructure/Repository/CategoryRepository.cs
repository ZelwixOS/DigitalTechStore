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
            return this.Context.Categories.Include(c => c.CommonCategory).AsNoTracking();
        }

        public Category GetItem(Guid id)
        {
            var category = this.Context.Categories
                .Include(c => c.Products)
                .Include(c => c.CommonCategory)
                .Include(c => c.CategoryParameterBlocks)
                    .ThenInclude(p => p.ParameterBlock)
                        .ThenInclude(b => b.Parameters)
                            .ThenInclude(p => p.ParameterValues)
                .FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                category.ParameterBlocks = category.CategoryParameterBlocks?.Select(p => p.ParameterBlock).ToHashSet();
            }

            return category;
        }

        public Category GetItem(string name)
        {
            var category = this.Context.Categories
                .Include(c => c.CategoryParameterBlocks)
                    .ThenInclude(p => p.ParameterBlock)
                        .ThenInclude(b => b.Parameters)
                            .ThenInclude(p => p.ParameterValues)
                .AsNoTracking()
                .FirstOrDefault(c => c.Name == name);
            if (category != null)
            {
                category.ParameterBlocks = category.CategoryParameterBlocks?.Select(p => p.ParameterBlock).ToHashSet();
            }

            return category;
        }

        public int DeleteItem(Category category)
        {
            if (this.GetItem(category.Id).Products.Count > 0)
            {
                return 0;
            }

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
