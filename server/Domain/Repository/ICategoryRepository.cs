namespace Domain.Repository
{
    using Domain.Models;

    public interface ICategoryRepository : IRepository<Category>
    {
        public Category GetItem(string name);
    }
}
