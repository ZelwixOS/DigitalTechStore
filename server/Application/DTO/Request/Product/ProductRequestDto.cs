namespace Application.DTO.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Domain.Models;
    using Microsoft.AspNetCore.Http;

    public abstract class ProductRequestDto : IDtoMapper<Product>
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? PriceWithoutDiscount { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(10)]
        public string VendorCode { get; set; }

        public IFormFile PicFile { get; set; }

        public string ParameterString { get; set; }

        public Guid CategoryId { get; set; }

        public abstract Product ToModel();
    }
}
