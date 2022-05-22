namespace Application.DTO.Request
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using Domain.Models;

    public class ProductUpdateRequestDto : ProductRequestDto
    {
        public Guid Id { get; set; }

        public override Product ToModel()
        {
            var parameterArray = JsonSerializer.Deserialize<ProductParameterCreateRequestDto[]>(this.ParameterString);

            var parameters = parameterArray?.Select(pa =>
            {
                pa.ProductId = this.Id;
                return pa.ToModel();
            });

            return new Product()
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price,
                Description = this.Description,
                VendorCode = this.VendorCode,
                CategoryIdFk = this.CategoryId,
                PriceWithoutDiscount = this.PriceWithoutDiscount,
                Category = null,
                ProductParameters = parameters.ToHashSet(),
            };
        }
    }
}
