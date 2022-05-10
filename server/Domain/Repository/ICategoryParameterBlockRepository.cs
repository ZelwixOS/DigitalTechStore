namespace Domain.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public interface ICategoryParameterBlockRepository : IRepository<CategoryParameterBlock, Guid>
    {
        int CreateItems(IEnumerable<CategoryParameterBlock> items);

        int DeletItems(IQueryable<CategoryParameterBlock> items);
    }
}
