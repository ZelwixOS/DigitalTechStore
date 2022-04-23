namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class TechParameterDto
    {
        public TechParameterDto(TechParameter techParam)
        {
            this.Id = techParam.Id;
            this.Name = techParam.Name;
            this.Important = techParam.Important;
            this.Range = techParam.Range;
            this.MinValue = techParam.MinValue;
            this.MaxValue = techParam.MaxValue;
            this.ParameterValues = techParam.ParameterValues != null
                ? techParam.ParameterValues.Select(p => new ParameterValueDto(p)).ToList()
                : null;
        }

        public TechParameterDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Important { get; set; }

        public bool Range { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public List<ParameterValueDto> ParameterValues { get; set; }
    }
}
