namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response.WithExtraInfo;
    using Application.ViewModels;

    public interface ICategoryService
    {
        List<CategoryDto> GetCategories();

        CategoryDto GetCategory(Guid id);

        WrapperExtraInfo<CategoryDto> GetCategory(string name, GetCategoryProductsRequest parameters);

        CategoryDto CreateCategory(CategoryCreateRequestDto category);

        int DeleteCategory(Guid id);

        CategoryDto UpdateCategory(CategoryUpdateRequestDto category);
    }
}
