namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.Interfaces;
    using Application.ViewModels;
    using Domain.Repository;

    public class ProductParameterService : IProductParameterService
    {
        private IProductParameterRepository _prodParamRepository;
        private IProductRepository _productRepository;
        private ITechParameterRepository _techParameterRepository;

        public ProductParameterService(IProductParameterRepository prodParamRepository, IProductRepository productRepository, ITechParameterRepository techParameterRepository)
        {
            _prodParamRepository = prodParamRepository;
            _productRepository = productRepository;
            _techParameterRepository = techParameterRepository;
        }

        public List<ProductParameterDto> GetProductParameters()
        {
            return _prodParamRepository.GetItems().Select(x => new ProductParameterDto(x)).ToList();
        }

        public List<ParameterOfProductDto> GetParametersOfProduct(Guid id)
        {
            var allParams = _prodParamRepository.GetItems(id);
            if (allParams.Count() > 0)
            {
                return allParams.Select(i => new ParameterOfProductDto(i)).ToList();
            }
            else
            {
                return new List<ParameterOfProductDto>();
            }
        }

        public ProductParameterDto CreateProductParameters(ProductParameterCreateRequestDto prodParam)
        {
            if (ProductParameterModelCheck(prodParam) && ProductParameterUniqueCheck(prodParam))
            {
                return new ProductParameterDto(_prodParamRepository.CreateItem(prodParam.ToModel()));
            }
            else
            {
                return null;
            }
        }

        public ProductParameterDto UpdateProductParameters(ProductParameterUpdateRequestDto prodParam)
        {
            if (ProductParameterModelCheck(prodParam))
            {
                return new ProductParameterDto(_prodParamRepository.UpdateItem(prodParam.ToModel()));
            }
            else
            {
                return null;
            }
        }

        public int DeleteProductParameters(Guid id)
        {
            var prodParam = _prodParamRepository.GetItem(id);
            if (prodParam != null)
            {
                return _prodParamRepository.DeleteItem(prodParam);
            }
            else
            {
                return 0;
            }
        }

        private bool ProductParameterModelCheck(ProductParameterRequestDto prodParam)
        {
            var product = _productRepository.GetItem(prodParam.ProductId);
            var parameter = _techParameterRepository.GetItem(prodParam.ParameterId);

            if (product != null && parameter != null && product.Category.Id == parameter.Category.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ProductParameterUniqueCheck(ProductParameterRequestDto prodParam)
        {
            var same = _prodParamRepository.GetItems(prodParam.ProductId).Where(i => i.ParameterIdFk == prodParam.ParameterId);

            if (same.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
