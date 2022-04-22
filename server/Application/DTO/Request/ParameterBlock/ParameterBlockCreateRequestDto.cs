namespace Application.DTO.Request.ParameterBlock
{
    using Domain.Models;

    public class ParameterBlockCreateRequestDto : ParameterBlockRequestDto
    {
        public override ParameterBlock ToModel()
        {
            return new ParameterBlock
            {
                Name = this.Name,
            };
        }
    }
}
