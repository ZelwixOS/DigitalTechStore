namespace Application.DTO.Response
{
    using System;
    using Domain.Models;

    public class ParameterOfProductDto
    {
        public ParameterOfProductDto(ProductParameter productParam)
        {
            this.Id = productParam.Id;
            this.Name = productParam.TechParameter.Name;
            this.ParameterId = productParam.ParameterIdFk;
            this.Value = productParam.TechParameter.Range ? productParam.Value.ToString() : productParam.ParameterValue.Value;
            this.ParameterValueId = productParam.TechParameter.Range ? null : productParam.ParameterValue.Id;
            this.Important = productParam.TechParameter.Important;
        }

        public ParameterOfProductDto()
        {
        }

        public Guid Id { get; set; }

        public Guid ParameterId { get; set; }

        public string Name { get; set; }

        public bool Important { get; set; }

        public string Value { get; set; }

        public Guid? ParameterValueId { get; set; }
    }
}
