namespace Application.DTO.Request.Cart
{
    using System;
    using Domain.Models;

    public class CartItemUpdateRequestDto : CartItemRequestDto
    {
        public Guid Id { get; set; }

        public override Cart ToModel()
        {
            return new Cart()
            {
                Id = this.Id,
                ProductId = this.ProductId,
                Count = this.Count,
            };
        }
    }
}
