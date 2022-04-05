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
            this.ProductId = prodParam.ProductIdFk;
            this.ParameterId = prodParam.ParameterIdFk;
        }

        public ProductParameterDto()
        {
        }

        public Guid Id { get; set; }

        public string Value { get; set; }

        public Guid ProductId { get; set; }

        public Guid ParameterId { get; set; }

        public HashSet<ProductOfCategoryDto> Products { get; set; }
    }
}
