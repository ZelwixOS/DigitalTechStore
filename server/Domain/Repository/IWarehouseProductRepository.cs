namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface IWarehouseProductRepository : IRepository<WarehouseProduct, Guid>
    {
        public WarehouseProduct GetItem(Guid productId, int unitId);
    }
}
