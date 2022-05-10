namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Request.ParameterBlock;
    using Application.DTO.Response;

    public interface ITechParameterService
    {
        List<TechParameterDto> GetTechParameters();

        public TechParameterDto CreateTechParameter(TechParameterCreateRequestDto techParam);

        TechParameterDto UpdatetTechParameter(TechParameterUpdateRequestDto techParam);

        int DeleteTechParameter(Guid id);

        List<ParameterBlockDto> GetParameterBlocks();

        public ParameterBlockDto CreateParameterBlock(ParameterBlockCreateRequestDto block);

        ParameterBlockDto UpdateParameterBlock(ParameterBlockUpdateRequestDto block);

        int DeleteParameterBlock(Guid id);

        public int LinkCategoryParameterBlock(Guid id, Guid categoryId);

        public int UnlinkCategoryParameterBlock(Guid id, Guid categoryId);

        public int SetBlockImportantStatus(Guid id, bool status);

        int SetCategoryParameterBlocks(List<CategoryParameterBlockRequestDto> blocks, Guid categoryId);
    }
}
