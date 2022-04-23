namespace Application.DTO.Request.ParameterValue
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Models;

    public class ParameterValueUpdateRequestDto : ParameterValueRequestDto
    {
        [Required]
        public Guid Id { get; set; }

        public override ParameterValue ToModel()
        {
            return new ParameterValue()
            {
                Id = this.Id,
                Value = this.Value,
                TechParameterIdFk = this.TechParameterId,
            };
        }
    }
}
