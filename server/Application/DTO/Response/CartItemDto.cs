namespace Application.DTO.Response
{
    using Domain.Models;

    public class CartItemDto
    {
        public CartItemDto(Cart cart, Product product)
        {
            this.Product = new ProductDto(product);
            this.Count = cart.Count;
        }

        public CartItemDto(int count, Product product)
        {
            this.Product = new ProductDto(product);
            this.Count = count;
        }

        public CartItemDto()
        {
        }

        public ProductDto Product { get; set; }

        public int Count { get; set; }
    }
}
