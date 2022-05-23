namespace Application.DTO.Response.Geography
{
    using Domain.Models;

    public class CityDto
    {
        public CityDto(City city)
        {
            this.Id = city.Id;
            this.Name = city.Name;
            this.RegionId = city.RegionId;
        }

        public CityDto()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }
    }
}
