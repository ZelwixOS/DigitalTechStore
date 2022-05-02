namespace Application.DTO.Response.Estate
{
    using Application.DTO.Response.Geography;
    using Domain.Models;

    public class OutletDto : EstateDto
    {
        public OutletDto(Outlet outlet)
        {
            this.Name = outlet.Name;
            this.City = outlet.City != null ? new CityDto(outlet.City) : null;
            this.Region = outlet.City != null && outlet.City.Region != null ? new RegionDto(outlet.City.Region) : null;
            this.PostalCode = outlet.PostalCode;
            this.Id = outlet.Id;
            this.Building = outlet.Building;
            this.StreetName = outlet.StreetName;
            this.UserNote = outlet.NoteForUser;
        }

        public OutletDto()
        {
        }

        public string UserNote { get; set; }
    }
}
