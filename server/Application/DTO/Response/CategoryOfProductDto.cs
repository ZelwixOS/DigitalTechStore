namespace Application.DTO.Response
{
    using System;
    using Domain.Models;

    public class CategoryOfProductDto : IComparable
    {
        public CategoryOfProductDto(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Description = category.Description;
        }

        public CategoryOfProductDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is CategoryOfProductDto expected)
            {
                if (expected.Name == this.Name && expected.Description == this.Description)
                {
                    return 0;
                }
            }

            return -1;
        }
    }
}
