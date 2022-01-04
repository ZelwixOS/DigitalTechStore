namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response.WithExtraInfo;
    using Application.ViewModels;

    public interface IProductService
    {
        WrapperExtraInfo<List<ProductDto>> GetProducts(GetProductsRequest parameters);

        ProductDto GetProduct(Guid id);

        ProductDto CreateProduct(ProductCreateRequestDto product);

        ProductDto UpdateProduct(ProductUpdateRequestDto product);

        int DeleteProduct(Guid id);
    }
}
