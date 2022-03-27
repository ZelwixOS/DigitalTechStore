namespace WebApi.Tests
{
    using System;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Domain.Models;

    public class TestObjectsFactory
    {
        public Category GetCategoryNotebooks()
        {
            return new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Notebooks",
                Description = "Not good, not bad...",
            };
        }

        public Category GetCategoryPhones()
        {
            return new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Phones",
                Description = "Yeah, of course, mobile phones...",
            };
        }

        public CategoryCreateRequestDto GetCategoryCreateRequestArgumentNotebooks()
        {
            return new CategoryCreateRequestDto()
            {
                Name = "Notebooks",
                Description = "Not good, not bad...",
            };
        }

        public CategoryUpdateRequestDto GetCategoryUpdateRequestDtoNotebooks(Guid id)
        {
            return new CategoryUpdateRequestDto()
            {
                Id = id,
                Name = "Notebooks",
                Description = "Not good, not bad...",
            };
        }

        public CategoryDto GetCategoryResponseNotebooks()
        {
            return new CategoryDto()
            {
                Name = "Notebooks",
                Description = "Not good, not bad...",
            };
        }

        public CategoryOfProductDto GetCategoryOfProductNotebooks()
        {
            return new CategoryOfProductDto()
            {
                Name = "Notebooks",
                Description = "Not good, not bad...",
            };
        }

        public Product GetProductNotebook(Guid categoryIdFk, Category category)
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Zelwix BookPad 21",
                Price = 333,
                Description = "This notebook doesn't exist",
                CategoryIdFk = categoryIdFk,
                Category = category,
                Mark = 4.7,
                PicURL = "21.jpg",
                VendorCode = "1234567",
            };
        }

        public Product GetProductNotebook2(Guid categoryIdFk, Category category)
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Zelwix BookPad 20",
                Price = 322,
                Description = "This notebook doesn't exist",
                CategoryIdFk = categoryIdFk,
                Category = category,
                Mark = 4.7,
                PicURL = "20.jpg",
                VendorCode = "2134567",
            };
        }

        public Product GetProductPhone(Guid categoryIdFk, Category category)
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Zelwix Phone 21",
                Price = 133,
                Description = "This phone doesn't exist",
                CategoryIdFk = category.Id,
                Category = category,
                Mark = 4.7,
                PicURL = "221.jpg",
                VendorCode = "2134567",
            };
        }

        public ProductCreateRequestDto GetProductCreateRequestDtoNotebook(Guid categoryIdFk)
        {
            return new ProductCreateRequestDto()
            {
                Name = "Zelwix BookPad 21",
                Price = 333,
                Description = "This notebook doesn't exist",
                CategoryId = categoryIdFk,
                Mark = 4.7,
                PicURL = "21.jpg",
                VendorCode = "1234567",
            };
        }

        public ProductUpdateRequestDto GetProductUpdateRequestDtoNotebook(Guid id, Guid categoryId)
        {
            return new ProductUpdateRequestDto()
            {
                Id = id,
                Name = "Zelwix BookPad 22",
                Price = 444,
                Description = "This notebook doesn't exist right now...",
                CategoryId = categoryId,
                Mark = 4.9,
                PicURL = "22.jpg",
                VendorCode = "1234666",
            };
        }

        public ProductDto GetProductDtoNotebook(CategoryOfProductDto category = null)
        {
            return new ProductDto()
            {
                Id = Guid.NewGuid(),
                Name = "Zelwix BookPad 21",
                Price = 333,
                Description = "This notebook doesn't exist",
                Category = category,
                Mark = 4.7,
                PicURL = "21.jpg",
                VendorCode = "1234567",
            };
        }
    }
}
