namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.DTO.Response.WithExtraInfo;
    using Application.Helpers;
    using Application.Interfaces;
    using Application.ViewModels;
    using Domain.Repository;

    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private ProductHelpersContainer _productHelper;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, ProductHelpersContainer helper)
        {
            _productHelper = helper;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public List<CategoryDto> GetCategories()
        {
            return _categoryRepository.GetItems().Select(x => new CategoryDto(x)).ToList();
        }

        public CategoryDto GetCategory(Guid id)
        {
            var category = _categoryRepository.GetItem(id);
            if (category != null)
            {
                return new CategoryDto(category);
            }
            else
            {
                return null;
            }
        }

        public WrapperExtraInfo<CategoryDto> GetCategory(string name, GetCategoryProductsRequest parameters)
        {
            var category = _categoryRepository.GetItem(name);
            if (category != null)
            {
                var filtersWithCodes = _productHelper.Filter.ConvertParameters(category.ParameterBlocks.SelectMany(p => p.Parameters), parameters.ParameterFilters);
                var allProducts = _productRepository.GetItems();
                var productByParams = _productHelper.Filter.FilterByParameters(allProducts, filtersWithCodes, category.Id);
                var filterResult = _productHelper.Filter.FilterByPrice(productByParams, (decimal)parameters.MinPrice, parameters.MaxPrice);
                var productsSorted = _productHelper.Sorter.SortProducts(filterResult.Products, parameters.SortingType, parameters.ReverseSorting);
                var prodList = _productHelper.Paginator.ElementsOfPage(productsSorted, parameters.PageNumber, parameters.ItemsOnPage);

                category.Products = new HashSet<Domain.Models.Product>(prodList.Products);

                return new WrapperExtraInfo<CategoryDto>(new CategoryDto(category), prodList.MaxPage, filterResult.MinPrice, filterResult.MaxPrice);
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
