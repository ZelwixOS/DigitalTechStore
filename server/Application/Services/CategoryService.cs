namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.DTO.Response.WithExtraInfo;
    using Application.Helpers;
    using Application.Interfaces;
    using Domain.Repository;
    using Microsoft.AspNetCore.Http;

    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private ICommonCategoryRepository _commonRepository;
        private IProductRepository _productRepository;
        private IParameterBlockRepository _parameterBlockRepository;
        private ProductHelpersContainer _productHelper;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, ICommonCategoryRepository commonRepository, IParameterBlockRepository parameterBlockRepository, ProductHelpersContainer helper)
        {
            _productHelper = helper;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _commonRepository = commonRepository;
            _parameterBlockRepository = parameterBlockRepository;
        }

        public List<CategoryDto> GetCategories()
        {
            return _categoryRepository.GetItems().Select(x => new CategoryDto(x, true)).ToList();
        }

        public List<CategoryDto> GetCategories(string commonName)
        {
            return _commonRepository.GetItem(commonName)?.Categories?.Select(x => new CategoryDto(x)).ToList();
        }

        public CategoryAllParameterBlocks GetCategoryBlocksInfo(Guid id)
        {
            var category = _categoryRepository.GetItem(id);
            var blocksInfo = new CategoryAllParameterBlocks();
            var allBlocks = _parameterBlockRepository.GetItems();
            if (category != null)
            {
                var includedIds = category.CategoryParameterBlocks.Select(cp => cp.ParameterBlockIdFk).ToList();
                blocksInfo.IncludedBlocks = category.CategoryParameterBlocks.Select(cp => new ParameterBlockDto(cp.ParameterBlock)).ToList();
                blocksInfo.ExcludedBlocks = allBlocks.Where(p => !includedIds.Contains(p.Id)).Select(p => new ParameterBlockDto(p, false)).ToList();
                return blocksInfo;
            }
            else
            {
                blocksInfo.ExcludedBlocks = allBlocks.Select(p => new ParameterBlockDto(p, false)).ToList();
                return blocksInfo;
            }
        }

        public CategoryDto GetCategory(Guid id)
        {
            var category = _categoryRepository.GetItem(id);
            if (category != null)
            {
                return new CategoryDto(category, true);
            }
            else
            {
                return null;
            }
        }

        public WrapperExtraInfo<CategoryDto> GetCategory(string name, GetCategoryProductsRequest parameters, IQueryCollection queryCollection)
        {
            var category = _categoryRepository.GetItem(name);
            if (category != null)
            {
                var allProducts = _productRepository.GetItems().Where(p => p.CategoryIdFk == category.Id && p.Published);
                var filters = _productHelper.Filter.ConvertParameters(queryCollection);
                allProducts = _productHelper.Filter.FilterByParameters(allProducts, filters.Range, filters.ListValue);
                var filterResult = _productHelper.Filter.FilterByPrice(allProducts, (decimal)parameters.MinPrice, parameters.MaxPrice);
                var productsSorted = _productHelper.Sorter.SortProducts(filterResult.Products, parameters.SortingType, parameters.ReverseSorting);
                var prodList = _productHelper.Paginator.ElementsOfPage(productsSorted, parameters.PageNumber, parameters.ItemsOnPage);

                category.Products = new HashSet<Domain.Models.Product>(prodList.Products);

                return new WrapperExtraInfo<CategoryDto>(new CategoryDto(category, true), prodList.MaxPage, filterResult.MinPrice, filterResult.MaxPrice);
            }
            else
            {
                return null;
            }
        }

        public CategoryDto CreateCategory(CategoryCreateRequestDto category)
        {
            return new CategoryDto(_categoryRepository.CreateItem(category.ToModel()));
        }

        public CategoryDto UpdateCategory(CategoryUpdateRequestDto category)
        {
            return new CategoryDto(_categoryRepository.UpdateItem(category.ToModel()));
        }

        public int DeleteCategory(Guid id)
        {
            var category = _categoryRepository.GetItem(id);
            if (category != null && (category.Products == null || category.Products.Count == 0))
            {
                return _categoryRepository.DeleteItem(category);
            }
            else
            {
                return 0;
            }
        }
    }
}
