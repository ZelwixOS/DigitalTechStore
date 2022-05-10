namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class CategoryDto : IComparable
    {
        public CategoryDto(Category category, bool onlyImportantParameters = false)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Description = category.Description;
            this.CommonCategoryName = category.CommonCategory?.Name;
            this.CommonCategoryId = category.CommonCategory?.Id;
            this.Products = category.Products?.Select(p => new ProductOfCategoryDto(p)).ToHashSet();

            var blocks = onlyImportantParameters
                ? category.CategoryParameterBlocks?.Where(cp => cp.ParameterBlock != null && cp.Important)
                : category.CategoryParameterBlocks?.Where(cp => cp.ParameterBlock != null);
            this.ParameterBlocks = blocks?
                .Where(pb => pb.ParameterBlock.Parameters.Any(p => p.Range || p.ParameterValues?.Count > 0))
                .Select(cp => new ParameterBlockDto(cp.ParameterBlock, onlyImportantParameters))
                    .ToList();
            this.DeliveryPrice = category.DeliveryCost;
        }

        public CategoryDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? CommonCategoryId { get; set; }

        public string CommonCategoryName { get; set; }

        public decimal DeliveryPrice { get; set; }

        public HashSet<ProductOfCategoryDto> Products { get; set; }

        public List<ParameterBlockDto> ParameterBlocks { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is CategoryDto expected)
            {
                if (expected.Name == this.Name && expected.Description == this.Description)
                {
                    if (this.Id == expected.Id)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }

            return -1;
        }
    }
}
