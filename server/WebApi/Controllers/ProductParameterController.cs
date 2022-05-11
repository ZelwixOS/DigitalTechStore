namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<List<ProductParameterDto>> Get()
        {
            return this.Ok(_productParameterService.GetProductParameters());
        }

        [HttpPost]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public ActionResult<TechParameterDto> Create([FromBody] ProductParameterCreateRequestDto prodParam)
        {
            return this.Ok(_productParameterService.CreateProductParameters(prodParam));
        }

        [HttpPut]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public ActionResult<TechParameterDto> Update([FromBody] ProductParameterUpdateRequestDto prodParam)
        {
            return this.Ok(_productParameterService.UpdateProductParameters(prodParam));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_productParameterService.DeleteProductParameters(id));
        }
    }
}
