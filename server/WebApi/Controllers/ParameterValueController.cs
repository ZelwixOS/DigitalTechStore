namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.ParameterValue;
    using Application.DTO.Response;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class ParameterValueController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private IParameterValueService _parameterValueService;

        public ParameterValueController(ILogger<ProductController> logger, IParameterValueService parameterValueService)
        {
            this.logger = logger;
            _parameterValueService = parameterValueService;
        }

        [HttpGet]
        public ActionResult<List<ParameterValueDto>> Get()
        {
            return this.Ok(_parameterValueService.GetParameterValues());
        }

        [HttpGet("{id}")]
        public ActionResult<ParameterValueDto> Get(Guid id)
        {
            return this.Ok(_parameterValueService.GetParameterValue(id));
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<ParameterValueDto> Create([FromBody] ParameterValueCreateRequestDto parameterValue)
        {
            return this.Ok(_parameterValueService.CreateParameterValue(parameterValue));
        }

        [HttpPut]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<ParameterValueDto> Update([FromBody] ParameterValueUpdateRequestDto parameterValue)
        {
            return this.Ok(_parameterValueService.UpdateParameterValue(parameterValue));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> Delete(Guid id)
        {
            return this.Ok(_parameterValueService.DeleteParameterValue(id));
        }
    }
}
