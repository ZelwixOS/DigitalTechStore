namespace Application.DTO.Response.Estate
{
    using Application.DTO.Response.Geography;

    public abstract class EstateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public RegionDto Region { get; set; }

        public CityDto City { get; set; }

        public string StreetName { get; set; }

        public string Building { get; set; }

        public string PostalCode { get; set; }
    }
}
