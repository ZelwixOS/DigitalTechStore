namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ParameterBlock
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public HashSet<CategoryParameterBlock> CategoryParameterBlocks { get; set; }

        public HashSet<TechParameter> Parameters { get; set; }
    }
}
