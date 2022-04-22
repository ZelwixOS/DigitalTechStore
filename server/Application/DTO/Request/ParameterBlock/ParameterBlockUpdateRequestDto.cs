namespace Application.DTO.Request.ParameterBlock
{
    using System;
    using Domain.Models;

    public class ParameterBlockUpdateRequestDto : ParameterBlockRequestDto
    {
        public Guid Id { get; set; }

        public override ParameterBlock ToModel()
        {
            return new ParameterBlock()
            {
                Id = this.Id,
                Name = this.Name,
            };
        }
    }
}
