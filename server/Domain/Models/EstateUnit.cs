namespace Domain.Models
{
    public abstract class EstateUnit
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public string StreetName { get; set; }

        public string Building { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public City City { get; set; }
    }
}
