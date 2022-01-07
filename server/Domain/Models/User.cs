﻿namespace Domain.Models
{
    using System;
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
    }
}