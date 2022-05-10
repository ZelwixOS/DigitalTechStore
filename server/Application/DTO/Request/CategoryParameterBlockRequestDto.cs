namespace Application.DTO.Request
{
    using System;
    using Domain.Models;

    public class CategoryParameterBlockRequestDto
    {
        public Guid ParameterBlockId { get; set; }

        public bool Important { get; set; }

        public CategoryParameterBlock ToModel(Guid categoryId)
        {
            return new CategoryParameterBlock()
            {
                CategoryIdFk = categoryId,
                Important = this.Important,
                ParameterBlockIdFk = this.ParameterBlockId,
            };
        }
    }
}
