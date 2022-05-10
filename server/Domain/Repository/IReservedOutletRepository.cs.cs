namespace Domain.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;

    public interface IReservedOutletRepository : IRepository<ReservedOutlet, Guid>
    {
        int DeleteItems(IQueryable<ReservedOutlet> items);
    }
}
