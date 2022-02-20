namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class TechParameterRequestDto : IDtoMapper<TechParameter>
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public bool Important { get; set; }

        public ParameterType ParameterType { get; set; }

        public abstract TechParameter ToModel();
    }
}
