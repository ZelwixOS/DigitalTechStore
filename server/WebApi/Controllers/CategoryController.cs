namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.DTO.Response.WithExtraInfo;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        private ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            this.logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<CategoryDto>> Get()
        {
            return this.Ok(_categoryService.GetCategories());
        }

        [HttpGet("id/{id}")]
        public ActionResult<CategoryDto> Get(Guid id)
        {
            return this.Ok(_categoryService.GetCategory(id));
        }

        [HttpGet("parameterBlocks/{id}")]
        public ActionResult<CategoryAllParameterBlocks> GetBlocksInfo(Guid id)
        {
            return this.Ok(_categoryService.GetCategoryBlocksInfo(id));
        }

        [HttpGet("{commonName}")]
        public ActionResult<CategoryDto> Get(string commonName)
        {
            return this.Ok(_categoryService.GetCategories(commonName));
        }

        [HttpGet("name/{name}")]
        public ActionResult<WrapperExtraInfo<CategoryDto>> Get(string name, [FromQuery] GetCategoryProductsRequest parameters)
        {
            var query = this.Request.Query;
            return this.Ok(_categoryService.GetCategory(name, parameters, query));
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<CategoryDto> Create([FromBody] CategoryCreateRequestDto category)
        {
            return this.Ok(_categoryService.CreateCategory(category));
        }

        [HttpPut]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<CategoryDto> Update([FromBody] CategoryUpdateRequestDto product)
        {
            return this.Ok(_categoryService.UpdateCategory(product));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_categoryService.DeleteCategory(id));
        }
    }
}
