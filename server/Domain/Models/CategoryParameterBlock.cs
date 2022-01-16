namespace Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoryParameterBlock
    {
        public Guid Id { get; set; }

        [Required]
        public bool Important { get; set; }

        [Required]
        public Guid CategoryIdFk { get; set; }

        [Required]
        public Guid ParameterBlockIdFk { get; set; }

        public Category Category { get; set; }

        public ParameterBlock ParameterBlock { get; set; }
    }
}
