namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface IPurchaseItemRepository : IRepository<PurchaseItem, Guid>
    {
    }
}
