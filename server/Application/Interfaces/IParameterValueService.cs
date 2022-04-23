namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.ParameterValue;
    using Application.DTO.Response;

    public interface IParameterValueService
    {
        public ParameterValueDto GetParameterValue(Guid id);

        public List<ParameterValueDto> GetParameterValues();

        public ParameterValueDto CreateParameterValue(ParameterValueCreateRequestDto parameterValue);

        public ParameterValueDto UpdateParameterValue(ParameterValueUpdateRequestDto parameterValue);

        public int DeleteParameterValue(Guid id);
    }
}
