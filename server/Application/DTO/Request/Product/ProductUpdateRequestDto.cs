namespace Application.DTO.Request
{
    using System;
    using Domain.Models;

    public class ProductUpdateRequestDto : ProductRequestDto
    {
        public Guid Id { get; set; }

        public override Product ToModel()
        {
            return new Product()
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price,
                Description = this.Description,
                Mark = this.Mark,
                Popularity = this.Popularity,
                VendorCode = this.VendorCode,
                PicURL = this.PicURL,
                CategoryIdFk = this.CategoryId,
                DiscountPrice = this.DiscountPrice,
                Category = null,
            };
        }
    }
}
