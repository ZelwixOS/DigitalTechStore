namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TechParameter
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public Guid ParameterBlockIdFk { get; set; }

        public bool Important { get; set; }

        [Required]
        public ParameterType ParameterType { get; set; }

        public HashSet<ProductParameter> ProductParameters { get; set; }

        public ParameterBlock ParameterBlock { get; set; }
    }
}
