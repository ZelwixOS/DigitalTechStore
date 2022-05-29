namespace Application.DTO.Request.Estate.Outlet
{
    using Application.Interfaces;
    using Domain.Models;

    public class OutletCreateRequestDto : EstateRequestDto, IDtoMapper<Outlet>
    {
        public string NoteForUser { get; set; }

        public Outlet ToModel()
        {
            return new Outlet()
            {
                CityId = this.CityId,
                StreetName = this.StreetName,
                Building = this.Building,
                PostalCode = this.PostalCode,
                Name = this.Name,
                NoteForUser = this.NoteForUser,
                PhoneNumber = this.PhoneNumber,
            };
        }
    }
}
