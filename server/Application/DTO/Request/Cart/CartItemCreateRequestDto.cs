namespace Application.DTO.Request.Cart
{
    using Domain.Models;

    public class CartItemCreateRequestDto : CartItemRequestDto
    {
        public override Cart ToModel()
        {
            return new Cart()
            {
                ProductId = this.ProductId,
                Count = this.Count,
            };
        }
    }
}
