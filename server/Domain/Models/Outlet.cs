namespace Domain.Models
{
    using System.Collections.Generic;

    public class Outlet : EstateUnit
    {
        public string NoteForUser { get; set; }

        public HashSet<OutletProduct> OutletProducts { get; set; }

        public HashSet<User> Workers { get; set; }

        public HashSet<Purchase> Purchases { get; set; }

        public HashSet<Purchase> DeliveredPurchases { get; set; }

        public HashSet<ReservedOutlet> ReservedProducts { get; set; }
    }
}
