namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Request.ParameterBlock;
    using Application.DTO.Response;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class ParameterBlockController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private ITechParameterService _techParameterService;

        public ParameterBlockController(ILogger<ProductController> logger, ITechParameterService techParameterService)
        {
            this.logger = logger;
            _techParameterService = techParameterService;
        }

        [HttpGet]
        public ActionResult<List<ParameterBlockDto>> Get()
        {
            return this.Ok(_techParameterService.GetParameterBlocks());
        }

        [HttpGet("{id}")]
        public ActionResult<List<TechParameterDto>> Get(Guid id)
        {
            return this.Ok(_techParameterService.GetParameterBlock(id));
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<ParameterBlockDto> Create([FromBody] ParameterBlockCreateRequestDto block)
        {
            return this.Ok(_techParameterService.CreateParameterBlock(block));
        }

        [HttpPost("include/{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<ParameterBlockDto> Include(Guid id, [FromBody] Guid categoryId)
        {
            return this.Ok(_techParameterService.LinkCategoryParameterBlock(id, categoryId));
        }

        [HttpPost("setMany/{categoryId}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<ParameterBlockDto> SetMany([FromRoute] Guid categoryId, [FromBody] List<CategoryParameterBlockRequestDto> blocks)
        {
            return this.Ok(_techParameterService.SetCategoryParameterBlocks(blocks, categoryId));
        }

        [HttpPost("important/{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<ParameterBlockDto> Important(Guid id, [FromBody] bool status)
        {
            return this.Ok(_techParameterService.SetBlockImportantStatus(id, status));
        }

        [HttpPost("remove/{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> Remove(Guid id, [FromBody] Guid categoryId)
        {
            return this.Ok(_techParameterService.UnlinkCategoryParameterBlock(id, categoryId));
        }

        [HttpPut]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<ParameterBlockDto> Update([FromBody] ParameterBlockUpdateRequestDto block)
        {
            return this.Ok(_techParameterService.UpdateParameterBlock(block));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_techParameterService.DeleteParameterBlock(id));
        }
    }
}
