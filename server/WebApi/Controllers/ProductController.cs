namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.DTO.Response.WithExtraInfo;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private IProductService _productService;
        private IProductParameterService _productParameterService;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IProductParameterService productParameterService)
        {
            this.logger = logger;
            _productService = productService;
            _productParameterService = productParameterService;
        }

        [HttpGet]
        public ActionResult<WrapperExtraInfo<List<ProductDto>>> Get([FromQuery] GetProductsRequest parameters)
        {
            return this.Ok(_productService.GetProducts(parameters));
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get(Guid id)
        {
            var product = _productService.GetProduct(id);
            if (product != null)
            {
                product.ProductParameter = _productParameterService.GetParametersOfProduct(product.Id);
            }

            return this.Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductDto> Create([FromBody] ProductCreateRequestDto product)
        {
            return this.Ok(_productService.CreateProduct(product));
        }

        [HttpPut]
        public ActionResult<ProductDto> Update([FromBody] ProductUpdateRequestDto product)
        {
            return this.Ok(_productService.UpdateProduct(product));
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_productService.DeleteProduct(id));
        }
    }
}
