namespace Application.DTO.Request
{
    using System;
    using Domain.Models;

    public class CategoryUpdateRequestDto : CategoryRequestDto
    {
        public Guid Id { get; set; }

        public override Category ToModel()
        {
            return new Category()
            {
                Id = this.Id,
                Name = this.Name,
                DeliveryCost = this.DeliveryPrice,
                Description = this.Description,
                CommonCategoryIdFk = this.CommonCategoryId,
                Products = null,
            };
        }
    }
}
