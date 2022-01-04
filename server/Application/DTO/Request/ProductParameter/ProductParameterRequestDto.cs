namespace Application.DTO.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class ProductParameterRequestDto : IDtoMapper<ProductParameter>
    {
        [Required]
        [MaxLength(30)]
        public string Value { get; set; }

        public Guid ParameterId { get; set; }

        public Guid ProductId { get; set; }

        public abstract ProductParameter ToModel();
    }
}