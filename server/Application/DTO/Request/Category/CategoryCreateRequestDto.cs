namespace Application.DTO.Request
{
    using Domain.Models;

    public class CategoryCreateRequestDto : CategoryRequestDto
    {
        public override Category ToModel()
        {
            return new Category()
            {
                Name = this.Name,
                Description = this.Description,
                CommonCategoryIdFk = this.CommonCategoryId,
                Products = null,
            };
        }
    }
}
