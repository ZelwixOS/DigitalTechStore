namespace Application.Interfaces
{
    using System;
    using Application.DTO.Request.Cart;
    using Application.DTO.Response;

    public interface ICustomerListsService
    {
        public CartItemDto AddOrUpdateCartItem(Guid userId, Guid productId, int count);

        public CartItemDto AddOrUpdateCartItem(CartItemRequestDto cartItem);

        public CartItemDto UpdateCartItem(CartItemUpdateRequestDto cartItem);

        public int DeleteCartItem(Guid userId, Guid productId);

        public int DeleteCartItem(Guid id);

        public CartDto GetUserCart(Guid userId);

        public int AddToWishlist(Guid userId, Guid productId);

        public int DeleteWish(Guid wishId);

        public int DeleteWish(Guid userId, Guid productId);

        public WishListDto GetUserWhishList(Guid userId);
    }
}
