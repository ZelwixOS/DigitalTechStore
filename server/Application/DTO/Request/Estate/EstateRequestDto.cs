namespace Application.DTO.Request.Estate
{
    public abstract class EstateRequestDto
    {
        public string Name { get; set; }

        public int CityId { get; set; }

        public string StreetName { get; set; }

        public string Building { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
