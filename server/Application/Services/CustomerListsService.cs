namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request.Cart;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class CustomerListsService : ICustomerListsService
    {
        private readonly ICartRepository cartRepository;
        private readonly IWishRepository wishRepository;
        private readonly IProductRepository productRepository;

        public CustomerListsService(ICartRepository cartRepository, IWishRepository wishRepository, IProductRepository productRepository)
        {
            this.cartRepository = cartRepository;
            this.wishRepository = wishRepository;
            this.productRepository = productRepository;
        }

        public CartItemDto AddOrUpdateCartItem(Guid userId, Guid productId, int count)
        {
            var cart = this.cartRepository.GetItems().Where(c => c.UserId == userId && c.ProductId == productId).FirstOrDefault();
            var product = productRepository.GetItem(productId);

            if (cart == null)
            {
                if (product != null)
                {
                    cart = new Cart()
                    {
                        ProductId = productId,
                        UserId = userId,
                        Count = count,
                    };

                    cart = this.cartRepository.CreateItem(cart);
                    return new CartItemDto(cart, product);
                }

                return null;
            }
            else
            {
                if (count != 0)
                {
                    cart.Count = count;
                    cart = this.cartRepository.UpdateItem(cart);
                }

                return new CartItemDto(cart, product);
            }
        }

        public int AddToWishlist(Guid userId, Guid productId)
        {
            var wish = this.wishRepository.GetItemsByUser(userId).FirstOrDefault(w => w.ProductId == productId);
            if (wish == null)
            {
                wish = new Wish()
                {
                    UserId = userId,
                    ProductId = productId,
                };

                this.wishRepository.CreateItem(wish);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteCartItem(Guid userId, Guid productId)
        {
            var cart = this.cartRepository.GetItemsByUser(userId).FirstOrDefault(c => c.ProductId == productId);
            if (cart != null)
            {
                this.cartRepository.DeleteItem(cart);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteCartItem(Guid id)
        {
            var cart = this.cartRepository.GetItem(id);
            if (cart != null)
            {
                this.cartRepository.DeleteItem(cart);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteWish(Guid wishId)
        {
            var wish = this.wishRepository.GetItem(wishId);
            if (wish != null)
            {
                this.wishRepository.DeleteItem(wish);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteWish(Guid userId, Guid productId)
        {
            var wish = this.wishRepository.GetItemsByUser(userId).FirstOrDefault(c => c.ProductId == productId);
            if (wish != null)
            {
                this.wishRepository.DeleteItem(wish);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public CartDto GetUserCart(Guid userId)
        {
            var productDictionary = this.productRepository.GetItems().ToDictionary(c => c.Id, c => c);
            var cartItems = this.cartRepository.GetItemsByUser(userId).Select(c => new CartItemDto(c.Count, productDictionary[c.ProductId]));
            return new CartDto(userId, cartItems.AsEnumerable());
        }

        public WishListDto GetUserWhishList(Guid userId)
        {
            var productDictionary = this.productRepository.GetItems().ToDictionary(c => c.Id, c => c);
            var wishes = this.wishRepository.GetItemsByUser(userId).Select(c => new ProductDto(productDictionary[c.ProductId]));
            return new WishListDto(userId, wishes.AsEnumerable());
        }

        public CartItemDto UpdateCartItem(CartItemUpdateRequestDto cartItem)
        {
            var cart = this.cartRepository.GetItem(cartItem.Id);
            var product = productRepository.GetItem(cartItem.ProductId);

            if (cart != null && product != null)
            {
                cart = this.cartRepository.UpdateItem(cartItem.ToModel());
                return new CartItemDto(cart, product);
            }
            else
            {
                return null;
            }
        }

        public List<ProductDto> MarkBoughtWished(List<ProductDto> products, Guid userId)
        {
            var inCart = this.cartRepository.GetItemsByUser(userId);
            var inWishlist = this.wishRepository.GetItemsByUser(userId);
            foreach (var product in products)
            {
                product.InCart = inCart.Any(c => c.ProductId == product.Id);
                product.InWishlist = inWishlist.Any(c => c.ProductId == product.Id);
            }

            return products;
        }

        public ProductDto MarkBoughtWished(ProductDto product, Guid userId)
        {
            product.InCart = this.cartRepository.GetItemsByUser(userId).Any(c => c.ProductId == product.Id);
            product.InWishlist = this.wishRepository.GetItemsByUser(userId).Any(c => c.ProductId == product.Id);

            return product;
        }
    }
}
