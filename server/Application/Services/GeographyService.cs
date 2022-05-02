 namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request.Geography.City;
    using Application.DTO.Request.Geography.Region;
    using Application.DTO.Response.Geography;
    using Application.Interfaces;
    using Domain.Repository;

    public class GeographyService : IGeographyService
    {
        private IRegionRepository _regionRepository;
        private ICityRepository _cityRepository;

        public GeographyService(IRegionRepository regionRepository, ICityRepository cityRepository)
        {
            _regionRepository = regionRepository;
            _cityRepository = cityRepository;
        }

        public List<RegionDto> GetRegionsAll()
        {
            return _regionRepository.GetItems().Select(o => new RegionDto(o, false)).ToList();
        }

        public List<RegionDto> GetRegionsWithOutlets()
        {
            return _regionRepository.GetItems().Where(r => r.Cities.Any(c => c.Outlets.Any())).Select(o => new RegionDto(o, true)).ToList();
        }

        public RegionDto GetCityRegion(int cityId)
        {
            var city = _cityRepository.GetItem(cityId);
            if (city == null)
            {
                return null;
            }

            return new RegionDto(city.Region);
        }

        public RegionDto CreateRegion(RegionCreateRequestDto region)
        {
            var regionModel = _regionRepository.CreateItem(region.ToModel());
            return new RegionDto(regionModel);
        }

        public CityDto CreateCity(CityCreateRequestDto city)
        {
            var cityModel = _cityRepository.CreateItem(city.ToModel());
            return new CityDto(cityModel);
        }

        public RegionDto UpdateRegion(RegionUpdateRequestDto region)
        {
            var regionModel = _regionRepository.GetItem(region.Id);
            if (regionModel == null)
            {
                regionModel = _regionRepository.UpdateItem(region.ToModel());
            }

            return new RegionDto(regionModel);
        }

        public CityDto UpdateCity(CityUpdateRequestDto city)
        {
            var cityModel = _cityRepository.GetItem(city.Id);
            if (cityModel == null)
            {
                cityModel = _cityRepository.UpdateItem(city.ToModel());
            }

            return new CityDto(cityModel);
        }

        public int DeleteRegion(int regionId)
        {
            var regionModel = _regionRepository.GetItem(regionId);
            return regionModel != null ? _regionRepository.DeleteItem(regionModel) : 0;
        }

        public int DeleteCity(int cityId)
        {
            var cityModel = _cityRepository.GetItem(cityId);
            return cityModel != null ? _cityRepository.DeleteItem(cityModel) : 0;
        }
    }
}
