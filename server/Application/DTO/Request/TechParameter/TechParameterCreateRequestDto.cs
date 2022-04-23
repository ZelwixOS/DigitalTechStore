namespace Application.DTO.Request
{
    using Domain.Models;

    public class TechParameterCreateRequestDto : TechParameterRequestDto
    {
        public override TechParameter ToModel()
        {
            return new TechParameter()
            {
                Name = this.Name,
                Important = this.Important,
                ParameterBlockIdFk = this.ParameterBlockId,
                Range = this.Range,
                MinValue = this.MinValue,
                MaxValue = this.MaxValue,
                ParameterBlock = null,
                ProductParameters = null,
            };
        }
    }
}
