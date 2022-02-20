namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request.CommonCategory;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Repository;

    public class CommonCategoryService : ICommonCategoryService
    {
        private ICommonCategoryRepository _commonCategoryRepository;

        public CommonCategoryService(ICommonCategoryRepository commonCategoryRepository)
        {
            _commonCategoryRepository = commonCategoryRepository;
        }

        public List<CommonCategoryDto> GetCommonCategories()
        {
            return _commonCategoryRepository.GetItems().Select(x => new CommonCategoryDto(x)).ToList();
        }

        public CommonCategoryDto GetCommonCategory(Guid id)
        {
            return new CommonCategoryDto(_commonCategoryRepository.GetItem(id));
        }

        public CommonCategoryDto GetCommonCategory(string name)
        {
            return new CommonCategoryDto(_commonCategoryRepository.GetItem(name));
        }

        public CommonCategoryDto CreateCommonCategory(CommonCategoryCreateRequestDto techParam)
        {
            return new CommonCategoryDto(_commonCategoryRepository.CreateItem(techParam.ToModel()));
        }

        public CommonCategoryDto UpdateCommonCategory(CommonCategoryUpdateRequestDto techParam)
        {
            return new CommonCategoryDto(_commonCategoryRepository.UpdateItem(techParam.ToModel()));
        }

        public int DeleteCommonCategory(Guid id)
        {
            var commonCategory = _commonCategoryRepository.GetItem(id);
            if (commonCategory != null && commonCategory.Categories.Count == 0)
            {
                return _commonCategoryRepository.DeleteItem(commonCategory);
            }
            else
            {
                return 0;
            }
        }
    }
}
