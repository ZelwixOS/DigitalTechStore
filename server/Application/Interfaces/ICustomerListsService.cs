namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.Cart;
    using Application.DTO.Response;

    public interface ICustomerListsService
    {
        public CartItemDto AddOrUpdateCartItem(Guid userId, Guid productId, int count);

        public CartItemDto UpdateCartItem(CartItemUpdateRequestDto cartItem);

        public int DeleteCartItem(Guid userId, Guid productId);

        public int DeleteCartItem(Guid id);

        public CartDto GetUserCart(Guid userId);

        public CartDto GetProductCart(string ids);

        public int AddToWishlist(Guid userId, Guid productId);

        public int DeleteWish(Guid wishId);

        public int DeleteWish(Guid userId, Guid productId);

        public WishListDto GetUserWhishList(Guid userId);

        public List<ProductDto> MarkBoughtWished(List<ProductDto> products, Guid userId);

        public ProductDto MarkBoughtWished(ProductDto product, Guid userId);
    }
}
