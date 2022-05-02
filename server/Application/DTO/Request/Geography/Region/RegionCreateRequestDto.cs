namespace Application.DTO.Request.Geography.Region
{
    using Application.Interfaces;
    using Domain.Models;

    public class RegionCreateRequestDto : GeographyRequestDto, IDtoMapper<Region>
    {
        public Region ToModel()
        {
            return new Region()
            {
                Name = this.Name,
            };
        }
    }
}
