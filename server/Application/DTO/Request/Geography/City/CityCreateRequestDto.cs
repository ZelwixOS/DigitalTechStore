namespace Application.DTO.Request.Geography.City
{
    using Application.Interfaces;
    using Domain.Models;

    public class CityCreateRequestDto : GeographyRequestDto, IDtoMapper<City>
    {
        public int RegionId { get; set; }

        public City ToModel()
        {
            return new City()
            {
                RegionId = RegionId,
                Name = this.Name,
            };
        }
    }
}
