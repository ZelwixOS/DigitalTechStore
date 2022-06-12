namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request.Review;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Repository;

    public class ReviewService : IReviewService
    {
        private IProductRepository _productRepository;
        private IReviewRepository _reviewRepository;
        private IUserRepository _userRepository;

        public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        public List<ReviewDto> GetProductReviews(Guid productId)
        {
            var reviews = _reviewRepository.GetItems().Where(r => r.ProductId == productId && r.Description != null && r.Description.Length > 0 && !r.Banned);
            var users = _userRepository.GetItems();

            return reviews.Select(r => new ReviewDto(r, r.User)).ToList();
        }

        public ReviewDto CreateReview(ReviewCreateRequestDto review, Guid userId)
        {
            var product = _productRepository.GetItem(review.ProductId);

            if (product == null)
            {
                return null;
            }

            product.Category = null;
            product.ProductParameters = null;
            product.CartItems = null;
            product.OutletProducts = null;
            product.WarehouseProducts = null;
            product.OutletsReserved = null;
            product.WarehousesReserved = null;
            product.PurchaseItems = null;

            product.Mark = ((product.Mark * product.Popularity) + review.Mark) / (product.Popularity + 1);
            product.Popularity++;

            _productRepository.UpdateItem(product);

            var reviewEntity = review.ToModel();
            reviewEntity.UserId = userId;

            var usr = _userRepository.GetItem(userId);

            var result = _reviewRepository.CreateItem(reviewEntity);
            return result != null ? new ReviewDto(result, usr) : null;
        }

        public ReviewDto UpdateReview(ReviewUpdateRequestDto review, Guid userId)
        {
            var reviewEntity = _reviewRepository.GetItem(review.ProductId, userId);

            if (reviewEntity == null)
            {
                return null;
            }

            var product = _productRepository.GetItem(review.ProductId);
            product.Mark = ((product.Mark * product.Popularity) - reviewEntity.Mark + review.Mark) / product.Popularity;

            reviewEntity.Mark = review.Mark;
            reviewEntity.Description = review.Description;

            _productRepository.UpdateItem(product);

            var result = _reviewRepository.UpdateItem(reviewEntity);
            return result != null ? new ReviewDto(result, result.User) : null;
        }

        public int DeleteReview(Guid productId, Guid userId)
        {
            var review = _reviewRepository.GetItem(productId, userId);
            if (review != null)
            {
                var product = _productRepository.GetItem(review.ProductId);
                product.Mark = ((product.Mark * product.Popularity) - review.Mark) / (product.Popularity - 1);
                product.Popularity--;

                _productRepository.UpdateItem(product);

                _reviewRepository.DeleteItem(review);
                return 1;
            }

            return 0;
        }

        public int BanReview(Guid id)
        {
            var review = _reviewRepository.GetItem(id);
            if (review == null)
            {
                return 0;
            }

            review.Banned = true;
            review = _reviewRepository.UpdateItem(review);

            return review == null ? 0 : 1;
        }
    }
}
