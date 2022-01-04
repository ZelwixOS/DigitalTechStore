namespace Application.DTO.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class ProductRequestDto : IDtoMapper<Product>
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public double Mark { get; set; }

        [Required]
        public int Popularity { get; set; }

        [Required]
        [MaxLength(10)]
        public string VendorCode { get; set; }

        [Required]
        [MaxLength(40)]
        public string PicURL { get; set; }

        public Guid CategoryId { get; set; }

        public abstract Product ToModel();
    }
}
