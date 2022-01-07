﻿namespace Application.DTO.Request
{
    using System;
    using Domain.Models;

    public class ProductParameterUpdateRequestDto : ProductParameterRequestDto
    {
        public Guid Id { get; set; }

        public override ProductParameter ToModel()
        {
            return new ProductParameter()
            {
                Id = this.Id,
                Value = this.Value,
                ParameterIdFk = this.ParameterId,
                ProductIdFk = this.ProductId,
                Product = null,
                Parameter = null,
            };
        }
    }
}