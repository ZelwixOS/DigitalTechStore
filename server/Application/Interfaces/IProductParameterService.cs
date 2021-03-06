namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;

    public interface IProductParameterService
    {
        List<ProductParameterDto> GetProductParameters();

        List<ParameterOfProductDto> GetParametersOfProduct(Guid id);

        List<ProductParameterBlockDto> GetProductParameters(Guid id);

        ProductParameterDto CreateProductParameters(ProductParameterCreateRequestDto prodParam);

        ProductParameterDto UpdateProductParameters(ProductParameterUpdateRequestDto prodParam);

        int DeleteProductParameters(Guid id);
    }
}
