namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using Application.DTO.Request.Geography.City;
    using Application.DTO.Request.Geography.Region;
    using Application.DTO.Response.Geography;
    using Application.Helpers;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class GeographyController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private IGeographyService geographyService;

        public GeographyController(ILogger<ProductController> logger, IGeographyService geographyService)
        {
            this.logger = logger;
            this.geographyService = geographyService;
        }

        [HttpGet("regions")]
        public ActionResult<List<RegionDto>> GetRegion()
        {
            var result = this.geographyService.GetRegionsWithOutlets();

            return this.Ok(result);
        }

        [HttpGet("region/{id}")]
        public ActionResult<List<RegionDto>> GetRegion(int id)
        {
            var result = this.geographyService.GetRegion(id);
            return this.Ok(result);
        }

        [HttpGet("allregions")]
        public ActionResult<List<RegionDto>> GetAllRegion()
        {
            var result = this.geographyService.GetRegionsAll();

            return this.Ok(result);
        }

        [HttpPost("region")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<RegionDto> CreateRegion([FromBody] RegionCreateRequestDto region)
        {
            var created = this.geographyService.CreateRegion(region);
            return this.Ok(created);
        }

        [HttpGet("cities/{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<CityDto> GetRegionCities(int id)
        {
            var created = this.geographyService.GetRegionCities(id);
            return this.Ok(created);
        }

        [HttpPost("city")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<CityDto> CreateCity([FromBody] CityCreateRequestDto city)
        {
            var created = this.geographyService.CreateCity(city);
            return this.Ok(created);
        }

        [HttpPut("region")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<RegionDto> UpdateRegion([FromBody] RegionUpdateRequestDto region)
        {
            var updated = this.geographyService.UpdateRegion(region);
            return this.Ok(updated);
        }

        [HttpGet("city/{id}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<CityDto> GetCity(int id)
        {
            var created = this.geographyService.GetCity(id);
            return this.Ok(created);
        }

        [HttpPut("city")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<CityDto> UpdateCity([FromBody] CityUpdateRequestDto city)
        {
            var updated = this.geographyService.UpdateCity(city);
            return this.Ok(updated);
        }

        [HttpDelete("region/{regionId}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> DeleteRegion(int regionId)
        {
            return this.Ok(this.geographyService.DeleteRegion(regionId));
        }

        [HttpDelete("city/{regionId}")]
        [Authorize(Roles = Constants.RoleManager.Admin)]
        public ActionResult<int> DeleteCity(int cityId)
        {
            return this.Ok(this.geographyService.DeleteCity(cityId));
        }
    }
}
