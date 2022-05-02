namespace Application.DTO.Request.Geography.Region
{
    using Application.Interfaces;
    using Domain.Models;

    public class RegionUpdateRequestDto : GeographyRequestDto, IDtoMapper<Region>
    {
        public int Id { get; set; }

        public Region ToModel()
        {
            return new Region()
            {
                Id = Id,
                Name = this.Name,
            };
        }
    }
}
