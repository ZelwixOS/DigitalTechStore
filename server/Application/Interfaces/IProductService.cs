namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.DTO.Response.WithExtraInfo;

    public interface IProductService
    {
        WrapperExtraInfo<List<ProductDto>> GetProducts(GetProductsRequest parameters, string search, bool withUnublished);

        ProductDto GetProduct(Guid id, int cityId, int regionId);

        int SetPublishProductStatus(Guid productId, bool publish);

        int DeleteProduct(Guid id);

        ProductDto Clone(Guid productId);

        Task<ProductDto> CreateProductAsync(ProductCreateRequestDto product, bool publish = false);

        Task<ProductDto> UpdateProductAsync(ProductUpdateRequestDto product);
    }
}
