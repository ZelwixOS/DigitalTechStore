namespace Application.DTO.Request.Estate.Warehouse
{
    using Application.Interfaces;
    using Domain.Models;

    public class WarehouseCreateRequestDto : EstateRequestDto, IDtoMapper<Warehouse>
    {
        public Warehouse ToModel()
        {
            return new Warehouse()
            {
                CityId = this.CityId,
                StreetName = this.StreetName,
                Building = this.Building,
                PostalCode = this.PostalCode,
                Name = this.Name,
                PhoneNumber = this.PhoneNumber,
            };
        }
    }
}
