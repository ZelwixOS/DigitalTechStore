namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
        private IProductService productService;
        private IProductParameterService productParameterService;
        private ICustomerListsService customerListsService;
        private IAccountService accountService;
        private IReviewService reviewService;
        private IGeographyService geographyService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            IProductParameterService productParameterService,
            ICustomerListsService customerListsService,
            IReviewService reviewService,
            IAccountService accountService,
            IGeographyService geographyService)
        {
            this.logger = logger;
            this.productService = productService;
            this.productParameterService = productParameterService;
            this.customerListsService = customerListsService;
            this.accountService = accountService;
            this.reviewService = reviewService;
            this.geographyService = geographyService;
        }

        [HttpGet]
        public async Task<ActionResult<WrapperExtraInfo<List<ProductDto>>>> Get([FromQuery] GetProductsRequest parameters)
        {
            var result = productService.GetProducts(parameters);

            var user = await this.accountService.GetCurrentUserAsync(HttpContext);

            if (user != null)
            {
                result.Container = this.customerListsService.MarkBoughtWished(result.Container, user.Id);
            }

            return this.Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetAsync(Guid id, int cityId)
        {
            var region = this.geographyService.GetCityRegion(cityId)?.Id ?? 0;
            var product = this.productService.GetProduct(id, cityId, region);

            var user = await this.accountService.GetCurrentUserAsync(HttpContext);

            if (user != null)
            {
                product = this.customerListsService.MarkBoughtWished(product, user.Id);
                product.Reviewed = this.reviewService.GetProductReviews(product.Id).Any(r => r.UserName == user.UserName);
            }

            return this.Ok(product);
        }

        [HttpGet("parameters/{id}")]
        public ActionResult<List<ProductParameterBlockDto>> GetProductParameters(Guid id)
        {
            return this.productParameterService.GetProductParameters(id);
        }

        [HttpPost]
        public ActionResult<ProductDto> Create([FromBody] ProductCreateRequestDto product)
        {
            return this.Ok(productService.CreateProduct(product));
        }

        [HttpPut]
        public ActionResult<ProductDto> Update([FromBody] ProductUpdateRequestDto product)
        {
            return this.Ok(productService.UpdateProduct(product));
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(productService.DeleteProduct(id));
        }
    }
}
