namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string SecondName { get; set; }

        [MaxLength(50)]
        public string GoogleMail { get; set; }

        [MaxLength(100)]
        public string Avatar { get; set; }

        public HashSet<Cart> CartItems { get; set; }

        public HashSet<Wish> WishedItems { get; set; }

        public HashSet<Review> Reviews { get; set; }

        public int? OutletId { get; set; }

        public int? WarehouseId { get; set; }

        public Outlet Outlet { get; set; }

        public Warehouse Warehouse { get; set; }

        public HashSet<Purchase> SoldItems { get; set; }

        public HashSet<Purchase> PurchasedItems { get; set; }
    }
}
