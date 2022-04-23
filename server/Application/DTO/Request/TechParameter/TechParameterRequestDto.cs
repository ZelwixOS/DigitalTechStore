namespace Application.DTO.Request
{
    using System;
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

        [Required]
        public Guid ParameterBlockId { get; set; }

        public bool Range { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public abstract TechParameter ToModel();
    }
}
