namespace Application.DTO.Request
{
    using Domain.Models;

    public class ProductParameterCreateRequestDto : ProductParameterRequestDto
    {
        public override ProductParameter ToModel()
        {
            return new ProductParameter()
            {
                Value = this.Value ?? 0,
                ParameterValueIdFk = this.ParameterValueId,
                ParameterIdFk = this.ParameterId,
                ProductIdFk = this.ProductId,
                Product = null,
                TechParameter = null,
            };
        }
    }
}
