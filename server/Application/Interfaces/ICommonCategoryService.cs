namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.CommonCategory;
    using Application.DTO.Response;

    public interface ICommonCategoryService
    {
        List<CommonCategoryDto> GetCommonCategories();

        public CommonCategoryDto GetCommonCategory(Guid id);

        public CommonCategoryDto GetCommonCategory(string name);

        public CommonCategoryDto CreateCommonCategory(CommonCategoryCreateRequestDto commonCategory);

        CommonCategoryDto UpdateCommonCategory(CommonCategoryUpdateRequestDto commonCategory);

        int DeleteCommonCategory(Guid id);
    }
}