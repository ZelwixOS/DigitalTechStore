namespace Domain.Repository
{
    using System;
    using Domain.Models;

    public interface IReviewRepository : IRepository<Review, Guid>
    {
        Review GetItem(Guid productId, Guid userId);
    }
}
