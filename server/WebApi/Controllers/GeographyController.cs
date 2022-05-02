namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using Application.DTO.Request.Geography.City;
    using Application.DTO.Request.Geography.Region;
    using Application.DTO.Response;
    using Application.DTO.Response.Geography;
    using Application.Interfaces;
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
        public ActionResult<List<ReviewDto>> GetRegion()
        {
            var result = this.geographyService.GetRegionsWithOutlets();

            return this.Ok(result);
        }

        [HttpGet("allregions")]
        public ActionResult<List<ReviewDto>> GetAllRegion()
        {
            var result = this.geographyService.GetRegionsAll();

            return this.Ok(result);
        }

        [HttpPost("region")]
        public ActionResult<RegionDto> CreateRegion([FromBody] RegionCreateRequestDto region)
        {
            var created = this.geographyService.CreateRegion(region);
            return this.Ok(created);
        }

        [HttpPost("city")]
        public ActionResult<RegionDto> CreateCity([FromBody] CityCreateRequestDto city)
        {
            var created = this.geographyService.CreateCity(city);
            return this.Ok(created);
        }

        [HttpPut("region")]
        public ActionResult<RegionDto> UpdateRegion([FromBody] RegionUpdateRequestDto region)
        {
            var updated = this.geographyService.UpdateRegion(region);
            return this.Ok(updated);
        }

        [HttpPut("city")]
        public ActionResult<RegionDto> UpdateCity([FromBody] CityUpdateRequestDto city)
        {
            var updated = this.geographyService.UpdateCity(city);
            return this.Ok(updated);
        }

        [HttpDelete("region/{regionId}")]
        public ActionResult<int> DeleteRegion(int regionId)
        {
            return this.Ok(this.geographyService.DeleteRegion(regionId));
        }

        [HttpDelete("city/{regionId}")]
        public ActionResult<int> DeleteCity(int cityId)
        {
            return this.Ok(this.geographyService.DeleteCity(cityId));
        }
    }
}
