namespace Application.DTO.Request.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WorkerUpdateDto
    {
        public Guid Id { get; set; }

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

        public int? OutletId { get; set; }

        public int? WarehouseId { get; set; }
    }
}
