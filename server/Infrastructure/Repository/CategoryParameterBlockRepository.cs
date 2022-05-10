namespace Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CategoryParameterBlockRepository : BaseRepository, ICategoryParameterBlockRepository
    {
        public CategoryParameterBlockRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public CategoryParameterBlock CreateItem(CategoryParameterBlock item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int CreateItems(IEnumerable<CategoryParameterBlock> items)
        {
            this.Context.AddRange(items);
            return this.Context.SaveChanges();
        }

        public int DeleteItem(CategoryParameterBlock item)
        {
            this.Context.CategoryParameterBlocks.Remove(item);
            return this.Context.SaveChanges();
        }

        public int DeletItems(IQueryable<CategoryParameterBlock> items)
        {
            this.Context.CategoryParameterBlocks.RemoveRange(items);
            return this.Context.SaveChanges();
        }

        public CategoryParameterBlock GetItem(Guid id)
        {
            var categoryParameterBlock = this.Context.CategoryParameterBlocks.FirstOrDefault(c => c.Id == id);
            return categoryParameterBlock;
        }

        public IQueryable<CategoryParameterBlock> GetItems()
        {
            return this.Context.CategoryParameterBlocks.AsNoTracking();
        }

        public CategoryParameterBlock UpdateItem(CategoryParameterBlock item)
        {
            var entity = this.Context.CategoryParameterBlocks.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
