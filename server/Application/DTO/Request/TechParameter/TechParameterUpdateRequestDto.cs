namespace Application.DTO.Request
{
    using System;
    using Domain.Models;

    public class TechParameterUpdateRequestDto : TechParameterRequestDto
    {
        public Guid Id { get; set; }

        public override TechParameter ToModel()
        {
            return new TechParameter()
            {
                Id = this.Id,
                Name = this.Name,
                Range = this.Range,
                MinValue = this.MinValue,
                MaxValue = this.MaxValue,
                ParameterBlockIdFk = this.ParameterBlockId,
                Important = this.Important,
            };
        }
    }
}