namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public decimal DeliveryCost { get; set; }

        public Guid CommonCategoryIdFk { get; set; }

        public HashSet<Product> Products { get; set; }

        public HashSet<CategoryParameterBlock> CategoryParameterBlocks { get; set; }

        public HashSet<ParameterBlock> ParameterBlocks { get; set; }

        public CommonCategory CommonCategory { get; set; }
    }
}
