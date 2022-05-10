namespace Application.DTO
{
    using System.Collections.Generic;
    using Application.DTO.Response;

    public class CategoryAllParameterBlocks
    {
        public List<ParameterBlockDto> IncludedBlocks { get; set; }

        public List<ParameterBlockDto> ExcludedBlocks { get; set; }
    }
}
