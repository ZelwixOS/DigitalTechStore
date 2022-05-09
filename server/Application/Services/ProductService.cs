namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.DTO.Response.WithExtraInfo;
    using Application.Helpers;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ProductHelpersContainer _productHelper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, ProductHelpersContainer helper)
        {
            _productHelper = helper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public WrapperExtraInfo<List<ProductDto>> GetProducts(GetProductsRequest parameters, string search)
        {
            var products = _productRepository.GetItems();
            IQueryable<Product> searchedResult = products;

            if (search != null && search != string.Empty)
            {
                var searchTags = search.Split(' ');
                foreach (var tag in searchTags)
                {
                    searchedResult = searchedResult.Where(p => p.Name.Contains(tag) || p.Category.Name.Contains(tag));
                }
            }

            var filterResult = _productHelper.Filter.FilterByPrice(searchedResult, parameters.MinPrice, parameters.MaxPrice);
            var productsSorted = _productHelper.Sorter.SortProducts(filterResult.Products, parameters.SortingType, parameters.ReverseSorting);
            var prodList = _productHelper.Paginator.ElementsOfPage(productsSorted, parameters.PageNumber, parameters.ItemsOnPage);

            return new WrapperExtraInfo<List<ProductDto>>(prodList.Products.Select(x => new ProductDto(x)).ToList(), prodList.MaxPage, filterResult.MinPrice, filterResult.MaxPrice);
        }

        public ProductDto GetProduct(Guid id, int cityId, int regionId)
        {
            var product = _productRepository.GetItem(id);
            if (product != null)
            {
                return new ProductDto(product, cityId, regionId);
            }
            else
            {
                return null;
            }
        }

        public ProductDto CreateProduct(ProductCreateRequestDto product)
        {
            if (ProductModelCheck(product))
            {
                return new ProductDto(_productRepository.CreateItem(product.ToModel()));
            }

            return null;
        }

        public ProductDto UpdateProduct(ProductUpdateRequestDto product)
        {
            if (ProductModelCheck(product) && product.Id != Guid.Empty)
            {
                return new ProductDto(_productRepository.UpdateItem(product.ToModel()));
            }

            return null;
        }

        public int DeleteProduct(Guid id)
        {
            var product = _productRepository.GetItem(id);
            if (product != null)
            {
                return _productRepository.DeleteItem(product);
            }
            else
            {
                return 0;
            }
        }

        private bool ProductModelCheck(ProductRequestDto product)
        {
            var category = _categoryRepository.GetItem(product.CategoryId);
            if (category != null && product.Price > 0 && product.Mark >= 0 && product.Mark <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
