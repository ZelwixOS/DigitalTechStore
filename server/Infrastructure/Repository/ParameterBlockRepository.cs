namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ParameterBlockRepository : BaseRepository, IParameterBlockRepository
    {
        public ParameterBlockRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public ParameterBlock CreateItem(ParameterBlock item)
        {
            var entity = this.Context.Add(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }

        public int DeleteItem(ParameterBlock item)
        {
            this.Context.ParameterBlocks.Remove(item);
            return this.Context.SaveChanges();
        }

        public ParameterBlock GetItem(Guid id)
        {
            var categoryParameterBlock = this.Context.ParameterBlocks.Include(b => b.Parameters).Include(b => b.CategoryParameterBlocks).FirstOrDefault(c => c.Id == id);
            return categoryParameterBlock;
        }

        public IQueryable<ParameterBlock> GetItems()
        {
            return this.Context.ParameterBlocks.AsNoTracking();
        }

        public ParameterBlock UpdateItem(ParameterBlock item)
        {
            var entity = this.Context.ParameterBlocks.Update(item);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
