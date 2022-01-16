namespace Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductParameter
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Value { get; set; }

        [Required]
        public Guid ParameterIdFk { get; set; }

        [Required]
        public Guid ProductIdFk { get; set; }

        public Product Product { get; set; }

        public TechParameter TechParameter { get; set; }
    }
}
