namespace Domain.Repository
{
    using System;
    using System.Linq;

    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetItems();

        T GetItem(Guid id);

        T CreateItem(T item);

        T UpdateItem(T item);

        int DeleteItem(T item);
    }
}
