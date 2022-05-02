namespace Application.DTO.Response.Estate
{
    using Application.DTO.Response.Geography;
    using Domain.Models;

    public class WarehouseDto : EstateDto
    {
        public WarehouseDto(Warehouse warehouse)
        {
            this.Name = warehouse.Name;
            this.City = warehouse.City != null ? new CityDto(warehouse.City) : null;
            this.Region = warehouse.City != null && warehouse.City.Region != null ? new RegionDto(warehouse.City.Region) : null;
            this.PostalCode = warehouse.PostalCode;
            this.Id = warehouse.Id;
            this.Building = warehouse.Building;
            this.StreetName = warehouse.StreetName;
        }

        public WarehouseDto()
        {
        }
    }
}
