namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface IPurchaseRepository : IRepository<Purchase, Guid>
    {
    }
}
