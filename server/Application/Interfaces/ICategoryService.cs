namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.DTO.Response.WithExtraInfo;
    using Microsoft.AspNetCore.Http;

    public interface ICategoryService
    {
        List<CategoryDto> GetCategories();

        List<CategoryDto> GetCategories(string commonName);

        CategoryDto GetCategory(Guid id);

        WrapperExtraInfo<CategoryDto> GetCategory(string name, GetCategoryProductsRequest parameters, IQueryCollection query);

        CategoryDto CreateCategory(CategoryCreateRequestDto category);

        int DeleteCategory(Guid id);

        CategoryDto UpdateCategory(CategoryUpdateRequestDto category);

        CategoryAllParameterBlocks GetCategoryBlocksInfo(Guid id);
    }
}
