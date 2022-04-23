namespace Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductParameter
    {
        public Guid Id { get; set; }

        public double Value { get; set; }

        public Guid? ParameterValueIdFk { get; set; }

        [Required]
        public Guid ParameterIdFk { get; set; }

        [Required]
        public Guid ProductIdFk { get; set; }

        public Product Product { get; set; }

        public TechParameter TechParameter { get; set; }

        public ParameterValue ParameterValue { get; set; }
    }
}
