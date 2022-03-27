namespace Application.DTO.Request.CommonCategory
{
    using Domain.Models;

    public class CommonCategoryCreateRequestDto : CommonCategoryRequestDto
    {
        public override CommonCategory ToModel()
        {
            return new CommonCategory()
            {
                Name = this.Name,
                Description = this.Description,
                Categories = null,
            };
        }
    }
}
