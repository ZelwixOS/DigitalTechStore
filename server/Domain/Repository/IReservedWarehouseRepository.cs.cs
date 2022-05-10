namespace Domain.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;

    public interface IReservedWarehouseRepository : IRepository<ReservedWarehouse, Guid>
    {
        int DeleteItems(IQueryable<ReservedWarehouse> items);
    }
}
