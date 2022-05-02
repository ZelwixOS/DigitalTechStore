namespace Application.DTO.Response.Geography
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public class RegionDto
    {
        public RegionDto(Region region, bool onlyOutletCities = false)
        {
            this.Id = region.Id;
            this.Name = region.Name;
            this.Cities = region.Cities != null
                ? (onlyOutletCities
                    ? region.Cities.Where(c => c.Outlets.Any()).Select(i => new CityDto(i)).ToList()
                    : region.Cities.Select(i => new CityDto(i)).ToList())
                : null;
        }

        public RegionDto()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<CityDto> Cities { get; set; }
    }
}
