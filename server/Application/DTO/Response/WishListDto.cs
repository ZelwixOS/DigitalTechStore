namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WishListDto
    {
        public WishListDto(Guid userId, IEnumerable<ProductDto> products)
        {
            this.UserId = userId;
            this.Products = products.ToList();
        }

        public WishListDto()
        {
        }

        public Guid UserId { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
