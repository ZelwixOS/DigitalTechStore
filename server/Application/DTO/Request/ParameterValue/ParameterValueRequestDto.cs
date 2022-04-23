namespace Application.DTO.Request.ParameterValue
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class ParameterValueRequestDto : IDtoMapper<ParameterValue>
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public Guid TechParameterId { get; set; }

        public abstract ParameterValue ToModel();
    }
}
