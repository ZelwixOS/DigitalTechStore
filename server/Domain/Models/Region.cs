namespace Domain.Models
{
    using System.Collections.Generic;

    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public HashSet<City> Cities { get; set; }

        public HashSet<Outlet> Outlets { get; set; }

        public HashSet<Warehouse> Warehouses { get; set; }
    }
}
