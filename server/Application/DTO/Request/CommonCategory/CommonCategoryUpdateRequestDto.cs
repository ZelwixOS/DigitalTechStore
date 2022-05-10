namespace Application.DTO.Request.CommonCategory
{
    using System;
    using Domain.Models;

    public class CommonCategoryUpdateRequestDto : CommonCategoryRequestDto
    {
        public Guid Id { get; set; }

        public override CommonCategory ToModel()
        {
            return new CommonCategory()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Categories = null,
            };
        }
    }
}
