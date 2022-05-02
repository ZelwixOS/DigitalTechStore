namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface IOutletProductRepository : IRepository<OutletProduct, Guid>
    {
        public OutletProduct GetItem(Guid productId, int unitId);
    }
}
