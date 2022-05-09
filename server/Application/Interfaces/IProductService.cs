namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.DTO.Response.WithExtraInfo;

    public interface IProductService
    {
        WrapperExtraInfo<List<ProductDto>> GetProducts(GetProductsRequest parameters, string search);

        ProductDto GetProduct(Guid id, int cityId, int regionId);

        ProductDto CreateProduct(ProductCreateRequestDto product);

        ProductDto UpdateProduct(ProductUpdateRequestDto product);

        int DeleteProduct(Guid id);
    }
}
