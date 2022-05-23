namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request.Geography.City;
    using Application.DTO.Request.Geography.Region;
    using Application.DTO.Response.Geography;

    public interface IGeographyService
    {
        CityDto CreateCity(CityCreateRequestDto city);

        RegionDto CreateRegion(RegionCreateRequestDto region);

        int DeleteCity(int cityId);

        int DeleteRegion(int regionId);

        List<RegionDto> GetRegionsAll();

        List<RegionDto> GetRegionsWithOutlets();

        RegionDto GetCityRegion(int cityId);

        CityDto UpdateCity(CityUpdateRequestDto city);

        RegionDto UpdateRegion(RegionUpdateRequestDto region);

        List<CityDto> GetRegionCities(int regionId);

        RegionDto GetRegion(int id);

        CityDto GetCity(int id);
    }
}