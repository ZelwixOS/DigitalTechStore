namespace Application.DTO.Request
{
    using System;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class ProductParameterRequestDto : IDtoMapper<ProductParameter>
    {
        public double? Value { get; set; }

        public Guid ParameterId { get; set; }

        public Guid ProductId { get; set; }

        public Guid? ParameterValueId { get; set; }

        public abstract ProductParameter ToModel();
    }
}