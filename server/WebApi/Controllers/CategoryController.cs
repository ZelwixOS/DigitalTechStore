﻿namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response.WithExtraInfo;
    using Application.Interfaces;
    using Application.ViewModels;
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

        [HttpGet("{id}")]
        public ActionResult<CategoryDto> Get(Guid id)
        {
            return this.Ok(_categoryService.GetCategory(id));
        }

        [HttpGet("name/{name}")]
        public ActionResult<WrapperExtraInfo<CategoryDto>> Get(string name, [FromQuery] GetCategoryProductsRequest parameters)
        {
            return this.Ok(_categoryService.GetCategory(name, parameters));
        }

        [HttpPost]
        public ActionResult<CategoryDto> Create([FromBody] CategoryCreateRequestDto category)
        {
            return this.Ok(_categoryService.CreateCategory(category));
        }

        [HttpPut]
        public ActionResult<CategoryDto> Update([FromBody] CategoryUpdateRequestDto product)
        {
            return this.Ok(_categoryService.UpdateCategory(product));
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_categoryService.DeleteCategory(id));
        }
    }
}