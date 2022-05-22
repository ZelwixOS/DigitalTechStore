namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class ProductParameterBlockDto
    {
        public ProductParameterBlockDto(ParameterBlock parameterBlock, IEnumerable<ProductParameter> parameters)
        {
            Id = parameterBlock.Id;
            Name = parameterBlock.Name;
            Parameters = parameters.Select(p => new ParameterOfProductDto(p)).ToList();
        }

        public ProductParameterBlockDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<ParameterOfProductDto> Parameters { get; set; }
    }
}
