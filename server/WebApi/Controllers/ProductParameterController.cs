namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.Interfaces;
    using Application.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductParameterController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private IProductParameterService _productParameterService;

        public ProductParameterController(ILogger<ProductController> logger, IProductParameterService productParameterService)
        {
            this.logger = logger;
            _productParameterService = productParameterService;
        }

        [HttpGet]
        public ActionResult<List<TechParameterDto>> Get()
        {
            return this.Ok(_productParameterService.GetProductParameters());
        }

        [HttpPost]
        public ActionResult<TechParameterDto> Create([FromBody] ProductParameterCreateRequestDto prodParam)
        {
            return this.Ok(_productParameterService.CreateProductParameters(prodParam));
        }

        [HttpPut]
        public ActionResult<TechParameterDto> Update([FromBody] ProductParameterUpdateRequestDto prodParam)
        {
            return this.Ok(_productParameterService.UpdateProductParameters(prodParam));
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_productParameterService.DeleteProductParameters(id));
        }
    }
}
