namespace Application.DTO.Response
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class ProductParameterBlockDto
    {
        public ProductParameterBlockDto(ParameterBlock parameterBlock, IEnumerable<ProductParameter> parameters)
        {
            Name = parameterBlock.Name;
            Parameters = parameters.Select(p => new ParameterOfProductDto(p)).ToList();
        }

        public ProductParameterBlockDto()
        {
        }

        public string Name { get; set; }

        public List<ParameterOfProductDto> Parameters { get; set; }
    }
}
