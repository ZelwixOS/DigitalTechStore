namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface ICommonCategoryRepository : IRepository<CommonCategory, Guid>
    {
        public CommonCategory GetItem(string name);
    }
}
