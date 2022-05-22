namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.DTO.Response.WithExtraInfo;
    using Application.Helpers;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class ProductService : IProductService
    {
        private const string PicPath = "ClientApp/products/";

        private IProductRepository _productRepository;
        private IProductParameterRepository _productParameterRepository;
        private ICategoryRepository _categoryRepository;
        private ProductHelpersContainer _productHelper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductParameterRepository productParameterRepository, ProductHelpersContainer helper)
        {
            _productHelper = helper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productParameterRepository = productParameterRepository;
        }

        public WrapperExtraInfo<List<ProductDto>> GetProducts(GetProductsRequest parameters, string search, bool withUnublished)
        {
            var products = _productRepository.GetItems();
            if (!withUnublished)
            {
                products = products.Where(x => x.Published);
            }

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

        public async Task<ProductDto> CreateProductAsync(ProductCreateRequestDto product, bool publish = false)
        {
            if (ProductModelCheck(product))
            {
                Product prod = product.ToModel();
                if (publish)
                {
                    prod.Published = true;
                }

                if (product.PicFile != null)
                {
                    var format = product.PicFile.FileName.Substring(product.PicFile.FileName.LastIndexOf('.'));
                    prod.PicURL = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss") + Guid.NewGuid() + format;

                    using (var fs = File.Create(PicPath + prod.PicURL))
                    {
                        await product.PicFile.CopyToAsync(fs);
                    }
                }

                var res = _productRepository.CreateItem(prod);

                return new ProductDto(res);
            }

            return null;
        }

        public int SetPublishProductStatus(Guid productId, bool publish)
        {
            var product = _productRepository.GetItem(productId);
            if (product == null)
            {
                return 0;
            }

            return this.SetPublishProductStatus(product, publish);
        }

        public ProductDto Clone(Guid productId)
        {
            var product = _productRepository.GetProductWithParameters(productId);
            if (product == null)
            {
                return null;
            }

            product.Category = null;
            product.Id = Guid.Empty;
            product.ProductParameters = product.ProductParameters?.Select(p =>
            {
                p.Id = Guid.Empty;
                p.Product = product;
                p.Product = null;
                p.ProductIdFk = Guid.Empty;
                p.TechParameter = null;
                p.ParameterValue = null;
                return p;
            }).ToHashSet();

            return new ProductDto(_productRepository.CreateItem(product));
        }

        public async Task<ProductDto> UpdateProductAsync(ProductUpdateRequestDto product)
        {
            if (ProductModelCheck(product) && product.Id != Guid.Empty)
            {
                var prodEntity = _productRepository.GetItem(product.Id);

                if (prodEntity == null)
                {
                    return null;
                }

                var prod = product.ToModel();
                prod.Mark = prodEntity.Mark;
                prod.Popularity = prodEntity.Popularity;
                prod.PicURL = prodEntity.PicURL;

                if (product.PicFile != null)
                {
                    var format = product.PicFile.FileName.Substring(product.PicFile.FileName.LastIndexOf('.'));
                    prod.PicURL = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss") + Guid.NewGuid() + format;

                    using (var fs = File.Create(PicPath + prod.PicURL))
                    {
                        await product.PicFile.CopyToAsync(fs);
                    }

                    var file = PicPath + prodEntity.PicURL;
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }

                var parameters = prod.ProductParameters;
                prod.ProductParameters = null;
                var productEntity = _productRepository.UpdateItem(prod);

                var existingParams = _productParameterRepository.GetItems(product.Id);
                _productParameterRepository.DeleteItems(existingParams);
                _productParameterRepository.CreateItems(parameters.AsQueryable());

                productEntity.ProductParameters = parameters;

                return new ProductDto(productEntity);
            }

            return null;
        }

        public int DeleteProduct(Guid id)
        {
            var product = _productRepository.GetItem(id);
            if (product != null)
            {
                var file = PicPath + product.PicURL;
                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                return _productRepository.DeleteItem(product);
            }
            else
            {
                return 0;
            }
        }

        private int SetPublishProductStatus(Product product, bool publish)
        {
            product.Published = publish;
            product.Category = null;
            product.WarehouseProducts = null;
            product.OutletProducts = null;
            product.CartItems = null;
            product.WishedItems = null;
            product.OutletsReserved = null;
            product.WarehousesReserved = null;
            return _productRepository.UpdateItem(product) != null ? 1 : 0;
        }

        private bool ProductModelCheck(ProductRequestDto product)
        {
            var category = _categoryRepository.GetItem(product.CategoryId);
            if (category != null && product.Price > 0)
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
