namespace Application.DTO.Request.Estate.Warehouse
{
    using Application.Interfaces;
    using Domain.Models;

    public class WarehouseUpdateRequestDto : EstateRequestDto, IDtoMapper<Warehouse>
    {
        public int Id { get; set; }

        public Warehouse ToModel()
        {
            return new Warehouse()
            {
                Id = this.Id,
                CityId = this.CityId,
                StreetName = this.StreetName,
                Building = this.Building,
                PostalCode = this.PostalCode,
                Name = this.Name,
            };
        }
    }
}
