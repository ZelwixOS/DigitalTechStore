namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO.Response.Statistics;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticService;
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public StatisticsController(IStatisticsService statisticService, IAccountService accountService, ILogger<StatisticsController> logger)
        {
            _statisticService = statisticService;
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet("monthOrders/{type}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public async Task<ActionResult<List<SalesTimeStatistics>>> GetOrdersForMonth(StatisticsType type, [FromQuery] bool finished)
        {
            var user = await _accountService.GetCurrentUserAsync(HttpContext);
            return this.Ok(_statisticService.GetOrdersForMonth(user, type, finished));
        }

        [HttpGet("monthSales/{type}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public async Task<ActionResult<SalesStatisticsForPeriod>> GetSalesForMonth(StatisticsType type)
        {
            var user = await _accountService.GetCurrentUserAsync(HttpContext);
            return this.Ok(_statisticService.GetSalesForMonth(user, type));
        }

        [HttpGet("totalSales/{type}")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public async Task<ActionResult<SalesStatisticsForPeriod>> GetSalesTotal(StatisticsType type)
        {
            var user = await _accountService.GetCurrentUserAsync(HttpContext);
            return this.Ok(_statisticService.GetSalesTotal(user, type));
        }

        [HttpGet("statForPeriod")]
        [Authorize(Roles = Constants.AuthManager.AdminManager)]
        public async Task<ActionResult<SalesStatisticsForPeriod>> GetStatFile([FromQuery] string fromDate, string toDate)
        {
            var user = await _accountService.GetCurrentUserAsync(HttpContext);
            var adress = _statisticService.GetStatFile(user, fromDate, toDate);
            if (adress != null)
            {
                adress = adress.Replace('\\', '/').Substring(1);
            }

            return this.Ok(adress);
        }
    }
}
