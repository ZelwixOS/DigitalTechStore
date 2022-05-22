namespace Application.DTO.Request
{
    using System.Linq;
    using System.Text.Json;
    using Domain.Models;

    public class ProductCreateRequestDto : ProductRequestDto
    {
        public override Product ToModel()
        {
            var parameterArray = JsonSerializer.Deserialize<ProductParameterCreateRequestDto[]>(this.ParameterString);

            return new Product()
            {
                Name = this.Name,
                Price = this.Price,
                Description = this.Description,
                Mark = 0,
                Popularity = 0,
                VendorCode = this.VendorCode,
                CategoryIdFk = this.CategoryId,
                PriceWithoutDiscount = this.PriceWithoutDiscount,
                Category = null,
                ProductParameters = parameterArray?.Select(pp => pp.ToModel()).ToHashSet(),
            };
        }
    }
}
