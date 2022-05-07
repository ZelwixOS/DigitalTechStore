namespace Domain.Models
{
    using System.Collections.Generic;

    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public HashSet<Outlet> Outlets { get; set; }

        public HashSet<Warehouse> Warehouses { get; set; }

        public HashSet<Delivery> Deliveries { get; set; }
    }
}
