namespace Application.DTO.Request.CommonCategory
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class CommonCategoryRequestDto : IDtoMapper<CommonCategory>
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public abstract CommonCategory ToModel();
    }
}
