namespace Application.DTO.Request
{
    using Domain.Models;

    public class ProductCreateRequestDto : ProductRequestDto
    {
        public override Product ToModel()
        {
            return new Product()
            {
                Name = this.Name,
                Price = this.Price,
                Description = this.Description,
                Mark = this.Mark,
                Popularity = this.Popularity,
                VendorCode = this.VendorCode,
                PicURL = this.PicURL,
                CategoryIdFk = this.CategoryId,
                Category = null,
            };
        }
    }
}
