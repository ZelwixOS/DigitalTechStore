namespace Application.DTO.Request.Estate.Outlet
{
    using Application.Interfaces;
    using Domain.Models;

    public class OutletUpdateRequestDto : EstateRequestDto, IDtoMapper<Outlet>
    {
        public int Id { get; set; }

        public string NoteForUser { get; set; }

        public Outlet ToModel()
        {
            return new Outlet()
            {
                Id = this.Id,
                CityId = this.CityId,
                StreetName = this.StreetName,
                Building = this.Building,
                PostalCode = this.PostalCode,
                Name = this.Name,
                NoteForUser = this.NoteForUser,
            };
        }
    }
}
