namespace Application.DTO.Request.Account
{
    using System.ComponentModel.DataAnnotations;

    public class WorkerRegistrationDto
    {
        [Required]
        [MaxLength(30)]
        public string Login { get; set; }

        [Required]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string SecondName { get; set; }

        [Required]
        public string RoleName { get; set; }

        public int? OutletId { get; set; }

        public int? WarehouseId { get; set; }
    }
}
