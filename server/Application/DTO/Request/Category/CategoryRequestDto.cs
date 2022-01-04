namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class CategoryRequestDto : IDtoMapper<Category>
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public abstract Category ToModel();
    }
}