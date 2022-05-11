namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO.Request.Account;
    using Application.DTO.Response;
    using Application.DTO.Response.Account;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ILogger logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            this.accountService = accountService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<MessageResultDto>> Register([FromBody] CustomerRegistrationDto model)
        {
            return this.Ok(await accountService.Register(model));
        }

        [HttpPost]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        [Route("RegisterWorker")]
        public async Task<ActionResult<MessageResultDto>> RegisterWorker([FromBody] WorkerRegistrationDto model)
        {
            return this.Ok(await accountService.Register(model));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<MessageResultDto>> Login([FromBody] LogInDto model)
        {
            return await accountService.Login(model);
        }

        [HttpPost]
        [Route("RegisterViaGoogle")]
        public async Task<ActionResult<MessageResultDto>> RegisterViaGoogle([FromBody] CustomerRegistrationDto model)
        {
            return this.Ok(await accountService.RegisterViaGoogle(model));
        }

        [HttpPost("GoogleAuth")]
        public async Task<ActionResult<MessageResultDto>> AuthViaGoogle([FromBody] GoogleLoginDto loginData)
        {
            var answer = await accountService.GoogleAuth(loginData.Token);
            return Ok(answer);
        }

        [HttpPost]
        [Route("LogOut")]
        public async Task<ActionResult<string>> LogOut()
        {
            return Ok(await accountService.LogOut());
        }

        [HttpGet]
        [Route("GetCurrentUserInfo")]
        public async Task<ActionResult<UserInfo>> GetCurrentUserInfo()
        {
            return Ok(await accountService.GetCurrentUserInfo(HttpContext));
        }

        [HttpGet]
        [Route("Role")]
        public async Task<ActionResult<string>> Role()
        {
            var roles = await accountService.GetRole(HttpContext);
            return Ok(roles[0]);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public async Task<ActionResult<WorkerInfo>> Get(Guid id)
        {
            var user = await accountService.GetWorker(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("Ban/{id}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public ActionResult<int> Ban(Guid id)
        {
            var res = accountService.BanUser(id);
            return Ok(res);
        }

        [HttpPost]
        [Route("Unban/{id}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public ActionResult<int> Unban(Guid id)
        {
            var res = accountService.UnBanUser(id);
            return Ok(res);
        }

        [HttpGet]
        [Route("GetWorkers")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public async Task<ActionResult<List<WorkerInfo>>> GetWorkers()
        {
            return Ok(await accountService.GetWorkersAsync(HttpContext));
        }

        [HttpPut]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public ActionResult<WorkerInfo> UpdateWorker(WorkerUpdateDto worker)
        {
            return Ok(accountService.UpdateWorker(worker));
        }

        [HttpPut("role/{id}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public async Task<ActionResult<WorkerInfo>> UpdateWorker(Guid id, string role)
        {
            return Ok(await accountService.UpdateWorkerRoleAsync(id, role));
        }
    }
}
