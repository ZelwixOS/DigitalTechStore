namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class CommonCategoryDto
    {
        public CommonCategoryDto(CommonCategory category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Description = category.Description;
            this.Categories = category.Categories == null ? null : category.Categories.Select(p => new CategoryDto(p)).ToHashSet();
        }

        public CommonCategoryDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public HashSet<CategoryDto> Categories { get; set; }
    }
}
