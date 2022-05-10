namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.CommonCategory;
    using Application.DTO.Response;

    using Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class CommonCategoryController : ControllerBase
    {
        private readonly ILogger<CommonCategoryController> logger;
        private ICommonCategoryService _commonCommonCategoryService;

        public CommonCategoryController(ILogger<CommonCategoryController> logger, ICommonCategoryService commonCommonCategoryService)
        {
            this.logger = logger;
            _commonCommonCategoryService = commonCommonCategoryService;
        }

        [HttpGet]
        public ActionResult<List<CommonCategoryDto>> Get([FromQuery]List<int> range)
        {
            return this.Ok(_commonCommonCategoryService.GetCommonCategories());
        }

        [HttpGet("{id}")]
        public ActionResult<CommonCategoryDto> Get(Guid id)
        {
            return this.Ok(_commonCommonCategoryService.GetCommonCategory(id));
        }

        [HttpGet("name/{name}")]
        public ActionResult<CommonCategoryDto> Get(string name)
        {
            return this.Ok(_commonCommonCategoryService.GetCommonCategory(name));
        }

        [HttpPost]
        public ActionResult<CommonCategoryDto> Create([FromBody] CommonCategoryCreateRequestDto commonCommonCategory)
        {
            return this.Ok(_commonCommonCategoryService.CreateCommonCategory(commonCommonCategory));
        }

        [HttpPut]
        public ActionResult<CommonCategoryDto> Update([FromBody] CommonCategoryUpdateRequestDto product)
        {
            return this.Ok(_commonCommonCategoryService.UpdateCommonCategory(product));
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_commonCommonCategoryService.DeleteCommonCategory(id));
        }
    }
}
