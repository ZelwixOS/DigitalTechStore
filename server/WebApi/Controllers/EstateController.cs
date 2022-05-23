namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.Estate.Outlet;
    using Application.DTO.Request.Estate.Warehouse;
    using Application.DTO.Response.Estate;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class EstateController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private IEstateService estateService;

        public EstateController(ILogger<ProductController> logger, IEstateService estateService)
        {
            this.logger = logger;
            this.estateService = estateService;
        }

        [HttpGet("outlets")]
        public ActionResult<List<OutletDto>> GetOutlets()
        {
            var result = this.estateService.GetOutlets();

            return this.Ok(result);
        }

        [HttpGet("warehouses")]
        public ActionResult<List<WarehouseDto>> GetWarehouses()
        {
            var result = this.estateService.GetWarehouses();

            return this.Ok(result);
        }

        [HttpGet("outlets/{cityId}")]
        public ActionResult<List<OutletDto>> GetOutletByCity(int cityId)
        {
            var result = this.estateService.GetOutlets(cityId);

            return this.Ok(result);
        }

        [HttpGet("warehouses/{regionId}")]
        public ActionResult<List<WarehouseDto>> GetWarehouseByRegion(int regionId)
        {
            var result = this.estateService.GetWarehouses(regionId);

            return this.Ok(result);
        }

        [HttpGet("warehouses/city/{cityId}")]
        public ActionResult<List<WarehouseDto>> GetWarehouseByCity(int cityId)
        {
            var result = this.estateService.GetWarehousesByCity(cityId);

            return this.Ok(result);
        }

        [HttpPost("outlet")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<OutletDto> CreateOutlet([FromBody] OutletCreateRequestDto outlet)
        {
            var created = this.estateService.CreateOutlet(outlet);
            return this.Ok(created);
        }

        [HttpPost("warehouse")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<WarehouseDto> CreateWarehouse([FromBody] WarehouseCreateRequestDto warehouse)
        {
            var created = this.estateService.CreateWarehouse(warehouse);
            return this.Ok(created);
        }

        [HttpPut("outlet")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<OutletDto> UpdateOutlet([FromBody] OutletUpdateRequestDto outlet)
        {
            var updated = this.estateService.UpdateOutlet(outlet);
            return this.Ok(updated);
        }

        [HttpPut("warehouse")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<WarehouseDto> UpdateWarehouse([FromBody] WarehouseUpdateRequestDto warehouse)
        {
            var updated = this.estateService.UpdateWarehouse(warehouse);
            return this.Ok(updated);
        }

        [HttpGet("outlet/{outletId}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<OutletDto> GetOutlet(int outletId)
        {
            return this.Ok(this.estateService.GetOutlet(outletId));
        }

        [HttpGet("warehouse/{warehouseId}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<WarehouseDto> GetWarehouse(int warehouseId)
        {
            return this.Ok(this.estateService.GetWarehouse(warehouseId));
        }

        [HttpDelete("outlet/{outletId}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> DeleteOutlet(int outletId)
        {
            return this.Ok(this.estateService.DeleteOutlet(outletId));
        }

        [HttpDelete("warehouse/{outletId}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> DeleteWarehouse(int warehouseId)
        {
            return this.Ok(this.estateService.DeleteWarehouse(warehouseId));
        }

        [HttpPost("setproduct")]
        [Authorize(Roles = Constants.AuthManager.WorkerNotCourier)]
        public ActionResult<WarehouseDto> SetWarehouseProduct(Guid productId, int unitId, bool outlet, int count)
        {
            var created = this.estateService.SetProductCount(productId, unitId, outlet, count);
            return this.Ok(created);
        }

        [HttpPost("addproduct")]
        [Authorize(Roles = Constants.AuthManager.WorkerNotCourier)]
        public ActionResult<WarehouseDto> AddWarehouseProduct(Guid productId, int unitId, bool outlet, int count)
        {
            var created = this.estateService.AddProductCount(productId, unitId, outlet, count);
            return this.Ok(created);
        }
    }
}
