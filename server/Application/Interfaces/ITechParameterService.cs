namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;

    public interface ITechParameterService
    {
        List<TechParameterDto> GetTechParameters();

        public TechParameterDto CreateTechParameter(TechParameterCreateRequestDto techParam);

        TechParameterDto UpdatetTechParameter(TechParameterUpdateRequestDto techParam);

        int DeleteTechParameter(Guid id);
    }
}
