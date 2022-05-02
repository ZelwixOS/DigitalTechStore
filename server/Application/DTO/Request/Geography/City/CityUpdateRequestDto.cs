namespace Application.DTO.Request.Geography.City
{
    using Application.Interfaces;
    using Domain.Models;

    public class CityUpdateRequestDto : GeographyRequestDto, IDtoMapper<City>
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public City ToModel()
        {
            return new City()
            {
                Id = Id,
                RegionId = RegionId,
                Name = this.Name,
            };
        }
    }
}
