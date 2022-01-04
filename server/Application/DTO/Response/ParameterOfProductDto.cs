namespace Application.ViewModels
{
    using System;
    using Domain.Models;

    public class ParameterOfProductDto
    {
        public ParameterOfProductDto(ProductParameter productParam)
        {
            this.Id = productParam.Id;
            this.Name = productParam.Parameter.Name;
            this.Value = productParam.Value;
            this.Important = productParam.Parameter.Important;
        }

        public ParameterOfProductDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Important { get; set; }

        public string Value { get; set; }
    }
}
