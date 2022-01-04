namespace Application.DTO.Request
{
    using Domain.Models;

    public class ProductParameterCreateRequestDto : ProductParameterRequestDto
    {
        public override ProductParameter ToModel()
        {
            return new ProductParameter()
            {
                Value = this.Value,
                ParameterIdFk = this.ParameterId,
                ProductIdFk = this.ProductId,
                Product = null,
                Parameter = null,
            };
        }
    }
}
