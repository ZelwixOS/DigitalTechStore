namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class CartDto
    {
        public CartDto(Guid userId, IEnumerable<CartItemDto> cartItems)
        {
            this.UserId = userId;
            this.Products = cartItems.ToList();
        }

        public CartDto(Guid userId, IEnumerable<Product> products, IEnumerable<Cart> cartItems)
        {
            this.UserId = userId;

            this.Products = new List<CartItemDto>();
            var productDictionary = products.ToDictionary(p => p.Id, p => p);
            Product product;
            foreach (var cartItem in cartItems)
            {
                if (productDictionary.TryGetValue(cartItem.ProductId, out product))
                {
                    this.Products.Add(new CartItemDto(cartItem, product));
                }
            }
        }

        public CartDto()
        {
        }

        public Guid UserId { get; set; }

        public List<CartItemDto> Products { get; set; }
    }
}
