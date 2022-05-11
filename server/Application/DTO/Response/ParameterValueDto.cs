namespace Application.DTO.Response
{
    using System;
    using Domain.Models;

    public class ParameterValueDto
    {
        public ParameterValueDto(ParameterValue parameterValue)
        {
            this.Id = parameterValue.Id;
            this.Value = parameterValue.Value;
            this.ParameterId = parameterValue.TechParameterIdFk;
            this.ParameterName = parameterValue.TechParameter?.Name;
        }

        public ParameterValueDto()
        {
        }

        public Guid Id { get; set; }

        public string Value { get; set; }

        public string ParameterName { get; set; }

        public Guid ParameterId { get; set; }
    }
}
