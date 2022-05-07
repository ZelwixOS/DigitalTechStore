namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO.Request.Purchase;
    using Application.DTO.Response;
    using Application.Helpers;
    using Application.Interfaces;
    using Domain.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private IPurchaseService purchaseService;
        private IAccountService accountService;

        public PurchaseController(ILogger<ProductController> logger, IPurchaseService purchaseService, IAccountService accountService)
        {
            this.logger = logger;
            this.purchaseService = purchaseService;
            this.accountService = accountService;
        }

        [Authorize(Roles = Constants.RoleManager.Admin)]
        [Authorize(Roles = Constants.RoleManager.Courier)]
        [Authorize(Roles = Constants.RoleManager.Manager)]
        [Authorize(Roles = Constants.RoleManager.ShopAssistant)]
        [HttpGet("{purchaseId}")]
        public ActionResult<PurchaseDto> Get(Guid purchaseId)
        {
            var result = purchaseService.GetPurchase(purchaseId);

            return this.Ok(result);
        }

        [Authorize(Roles = Constants.RoleManager.Admin)]
        [HttpGet("userPurchases/{purchaseId}")]
        public ActionResult<List<PurchaseDto>> GetUserPurchases(Guid userId)
        {
            var result = purchaseService.GetUserPurchases(userId);

            return this.Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseDto>>> Get()
        {
            var user = await accountService.GetCurrentUserAsync(HttpContext);
            var result = purchaseService.GetUserPurchases(user.Id);

            return this.Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> Create([FromBody] PurchaseCreateRequestDto purchase)
        {
            var user = await accountService.GetCurrentUserAsync(HttpContext);
            PurchaseDto result;
            if (user != null)
            {
                result = purchaseService.CreatePurchaseByUser(purchase, user);
            }
            else
            {
                result = purchaseService.CreatePurchaseByGuest(purchase);
            }

            return this.Ok(result);
        }

        [Authorize(Roles = Constants.RoleManager.Admin)]
        [Authorize(Roles = Constants.RoleManager.Courier)]
        [Authorize(Roles = Constants.RoleManager.Manager)]
        [Authorize(Roles = Constants.RoleManager.ShopAssistant)]
        [HttpPut]
        public ActionResult<int> UpdateStatus([FromBody] Guid purchaseId, PurchaseStatus status)
        {
            var result = purchaseService.ChangePurchaseStatus(purchaseId, status);
            return this.Ok(result);
        }
    }
}
