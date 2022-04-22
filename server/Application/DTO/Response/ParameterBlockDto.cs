namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class ParameterBlockDto
    {
        public ParameterBlockDto(ParameterBlock parameterBlock)
        {
            Id = parameterBlock.Id;
            Name = parameterBlock.Name;
            Parameters = parameterBlock.Parameters?.Select(p => new TechParameterDto(p)).ToList();
        }

        public ParameterBlockDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TechParameterDto> Parameters { get; set; }
    }
}
