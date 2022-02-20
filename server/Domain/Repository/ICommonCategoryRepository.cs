namespace Domain.Repository
{
    using Domain.Models;

    public interface ICommonCategoryRepository : IRepository<CommonCategory>
    {
        public CommonCategory GetItem(string name);
    }
}
