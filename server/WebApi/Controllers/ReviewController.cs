namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO.Request.Review;
    using Application.DTO.Response;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private IReviewService reviewService;
        private IAccountService accountService;

        public ReviewController(ILogger<ProductController> logger, IReviewService reviewService, IAccountService accountService)
        {
            this.logger = logger;
            this.reviewService = reviewService;
            this.accountService = accountService;
        }

        [HttpGet("{productId}")]
        public ActionResult<List<ReviewDto>> Get(Guid productId)
        {
            var result = reviewService.GetProductReviews(productId);

            return this.Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateAsync([FromBody] ReviewCreateRequestDto review)
        {
            var user = await this.accountService.GetCurrentUserAsync(HttpContext);

            if (user == null)
            {
                return this.Unauthorized();
            }

            return this.Ok(reviewService.CreateReview(review, user.Id));
        }

        [HttpPut]
        public async Task<ActionResult<ReviewDto>> UpdateAsync([FromBody] ReviewUpdateRequestDto review)
        {
            var user = await this.accountService.GetCurrentUserAsync(HttpContext);

            if (user == null)
            {
                return this.Unauthorized();
            }

            return this.Ok(reviewService.UpdateReview(review, user.Id));
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<int>> DeleteAsync(Guid productId)
        {
            var user = await this.accountService.GetCurrentUserAsync(HttpContext);

            if (user == null)
            {
                return this.Unauthorized();
            }

            return this.Ok(reviewService.DeleteReview(productId, user.Id));
        }

        [HttpPost("ban/{id}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public ActionResult<int> BanComment(Guid id)
        {
            return this.Ok(reviewService.BanReview(id));
        }
    }
}
