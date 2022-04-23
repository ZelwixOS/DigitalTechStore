namespace Application.DTO.Request.ParameterValue
{
    using Domain.Models;

    public class ParameterValueCreateRequestDto : ParameterValueRequestDto
    {
        public override ParameterValue ToModel()
        {
            return new ParameterValue()
            {
                Value = this.Value,
                TechParameterIdFk = this.TechParameterId,
            };
        }
    }
}
