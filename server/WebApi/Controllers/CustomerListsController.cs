namespace WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.DTO.Request.Cart;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerListsController : ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        private ICustomerListsService customerListsService;
        private IAccountService accountService;

        public CustomerListsController(ILogger<CategoryController> logger, ICustomerListsService customerListsService, IAccountService accountService)
        {
            this.logger = logger;
            this.customerListsService = customerListsService;
            this.accountService = accountService;
        }

        [HttpGet("cart")]
        public async Task<ActionResult<CartDto>> GetCartAsync()
        {
            var user = await this.accountService.GetCurrentUserAsync(HttpContext);
            return this.Ok(customerListsService.GetUserCart(user.Id));
        }

        [HttpGet("wishlist")]
        public async Task<ActionResult<WishListDto>> GetWishlistAsync()
        {
            var user = await this.accountService.GetCurrentUserAsync(HttpContext);
            return this.Ok(customerListsService.GetUserWhishList(user.Id));
        }

        [HttpPost("cart")]
        public ActionResult<CartItemDto> CreateCartItem(CartItemRequestDto cart)
        {
            return this.Ok(customerListsService.AddOrUpdateCartItem(cart));
        }

        [HttpPut("cart")]
        public ActionResult<CartItemDto> UpdateCartItem(CartItemUpdateRequestDto cart)
        {
            return this.Ok(customerListsService.UpdateCartItem(cart));
        }

        [HttpPost("wishlist")]
        public async Task<ActionResult<CartItemDto>> AddWishAsync(Guid productId)
        {
            var user = await this.accountService.GetCurrentUserAsync(HttpContext);
            return this.Ok(customerListsService.AddToWishlist(user.Id, productId));
        }

        [HttpDelete("cart/{productId}")]
        public async Task<ActionResult<CartItemDto>> DeleteCartItemAsync(Guid productId)
        {
            var user = await this.accountService.GetCurrentUserAsync(HttpContext);
            return this.Ok(customerListsService.DeleteCartItem(user.Id, productId));
        }

        [HttpDelete("wishlist/{productId}")]
        public async Task<ActionResult<CartItemDto>> DeleteWishlistItemAsync(Guid productId)
        {
            var user = await this.accountService.GetCurrentUserAsync(HttpContext);
            return this.Ok(customerListsService.DeleteWish(user.Id, productId));
        }
    }
}
