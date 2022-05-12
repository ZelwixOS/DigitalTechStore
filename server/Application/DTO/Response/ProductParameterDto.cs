namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using Domain.Models;

    public class ProductParameterDto
    {
        public ProductParameterDto(ProductParameter prodParam)
        {
            this.Id = prodParam.Id;
            this.Value = prodParam.Value;
            this.ParameterValue = prodParam.ParameterValue?.Value;
            this.ParameterValueId = prodParam.ParameterValueIdFk;
            this.ProductId = prodParam.ProductIdFk;
            this.ParameterId = prodParam.ParameterIdFk;
            this.ProductName = prodParam.Product?.Name;
            this.ParameterName = prodParam.TechParameter?.Name;
        }

        public ProductParameterDto()
        {
        }

        public Guid Id { get; set; }

        public double Value { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public Guid ParameterId { get; set; }

        public string ParameterValue { get; set; }

        public Guid? ParameterValueId { get; set; }

        public string ParameterName { get; set; }

        public HashSet<ProductOfCategoryDto> Products { get; set; }
    }
}
