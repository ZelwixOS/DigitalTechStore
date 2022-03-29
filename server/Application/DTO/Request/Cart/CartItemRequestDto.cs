namespace Application.DTO.Request.Cart
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class CartItemRequestDto : IDtoMapper<Cart>
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        public abstract Cart ToModel();
    }
}
