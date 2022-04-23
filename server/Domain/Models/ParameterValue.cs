namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ParameterValue
    {
        public Guid Id { get; set; }

        [Required]
        public Guid TechParameterIdFk { get; set; }

        public string Value { get; set; }

        public TechParameter TechParameter { get; set; }

        public HashSet<ProductParameter> ProductParameters { get; set; }
    }
}
