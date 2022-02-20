namespace Application.DTO.Request.CommonCategory
{
    using Domain.Models;

    public class CommonCategoryUpdateRequestDto : CommonCategoryRequestDto
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
