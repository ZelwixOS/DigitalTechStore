namespace Application.DTO.Request.ParameterBlock
{
    using Application.Interfaces;
    using Domain.Models;

    public abstract class ParameterBlockRequestDto : IDtoMapper<ParameterBlock>
    {
        public string Name { get; set; }

        public abstract ParameterBlock ToModel();
    }
}
