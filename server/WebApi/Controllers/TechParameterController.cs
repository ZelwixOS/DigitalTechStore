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
    public class TechParameterController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private ITechParameterService _techParameterService;

        public TechParameterController(ILogger<ProductController> logger, ITechParameterService techParameterService)
        {
            this.logger = logger;
            _techParameterService = techParameterService;
        }

        [HttpGet]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<List<TechParameterDto>> Get()
        {
            return this.Ok(_techParameterService.GetTechParameters());
        }

        [HttpGet("lists")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<List<TechParameterDto>> GetLists()
        {
            return this.Ok(_techParameterService.GetTechListParameters());
        }

        [HttpGet("{id}")]
        public ActionResult<TechParameterDto> Get(Guid id)
        {
            return this.Ok(_techParameterService.GetTechParameter(id));
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<TechParameterDto> Create([FromBody] TechParameterCreateRequestDto techParam)
        {
            return this.Ok(_techParameterService.CreateTechParameter(techParam));
        }

        [HttpPut]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<TechParameterDto> Update([FromBody] TechParameterUpdateRequestDto techParam)
        {
            return this.Ok(_techParameterService.UpdatetTechParameter(techParam));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_techParameterService.DeleteTechParameter(id));
        }
    }
}
