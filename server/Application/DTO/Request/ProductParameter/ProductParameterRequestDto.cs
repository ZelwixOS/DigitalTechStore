namespace Application.DTO.Request
{
    using System;
    using System.Text.Json.Serialization;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class ProductParameterRequestDto : IDtoMapper<ProductParameter>
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("parameterId")]
        public Guid ParameterId { get; set; }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("parameterValueId")]
        public Guid? ParameterValueId { get; set; }

        public abstract ProductParameter ToModel();
    }
}