namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.Review;
    using Application.DTO.Response;

    public interface IReviewService
    {
        public List<ReviewDto> GetProductReviews(Guid productId);

        public ReviewDto CreateReview(ReviewCreateRequestDto review, Guid userId);

        public ReviewDto UpdateReview(ReviewUpdateRequestDto review, Guid userId);

        public int BanReview(Guid id);

        public int DeleteReview(Guid productId, Guid userId);
    }
}