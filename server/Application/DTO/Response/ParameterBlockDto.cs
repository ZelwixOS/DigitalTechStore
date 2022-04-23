namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class ParameterBlockDto
    {
        public ParameterBlockDto(ParameterBlock parameterBlock, bool onlyImportantParams = false)
        {
            Id = parameterBlock.Id;
            Name = parameterBlock.Name;
            Parameters = onlyImportantParams
                ? parameterBlock.Parameters?.Where(p => p.Important).Select(p => new TechParameterDto(p)).ToList()
                : parameterBlock.Parameters?.Select(p => new TechParameterDto(p)).ToList();
        }

        public ParameterBlockDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TechParameterDto> Parameters { get; set; }
    }
}
