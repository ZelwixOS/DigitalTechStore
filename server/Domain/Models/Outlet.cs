namespace Domain.Models
{
    using System.Collections.Generic;

    public class Outlet : EstateUnit
    {
        public string NoteForUser { get; set; }

        public HashSet<OutletProduct> OutletProducts { get; set; }
    }
}
