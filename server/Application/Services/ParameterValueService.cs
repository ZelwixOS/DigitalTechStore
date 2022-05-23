namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request.ParameterValue;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Repository;

    public class ParameterValueService : IParameterValueService
    {
        private IParameterValueRepository _parameterValueRepository;
        private ITechParameterRepository _techParameterRepository;

        public ParameterValueService(IParameterValueRepository parameterValueRepository, ITechParameterRepository techParameterRepository)
        {
            _parameterValueRepository = parameterValueRepository;
            _techParameterRepository = techParameterRepository;
        }

        public ParameterValueDto CreateParameterValue(ParameterValueCreateRequestDto parameterValue)
        {
            var techParam = _techParameterRepository.GetItem(parameterValue.TechParameterId);
            if (techParam == null)
            {
                return null;
            }

            return new ParameterValueDto(_parameterValueRepository.CreateItem(parameterValue.ToModel()));
        }

        public int DeleteParameterValue(Guid id)
        {
            var parameterValue = _parameterValueRepository.GetItem(id);
            if (parameterValue == null)
            {
                return 0;
            }

            return _parameterValueRepository.DeleteItem(parameterValue);
        }

        public ParameterValueDto GetParameterValue(Guid id)
        {
            var parameterValue = _parameterValueRepository.GetItem(id);
            return parameterValue != null ? new ParameterValueDto(parameterValue) : null;
        }

        public List<ParameterValueDto> GetValuesByParameter(Guid id)
        {
            var parameterValues = _parameterValueRepository.GetItems().Where(p => p.TechParameterIdFk == id).Select(p => new ParameterValueDto(p)).ToList();
            return parameterValues;
        }

        public List<ParameterValueDto> GetParameterValues()
        {
            var parameterValues = _parameterValueRepository.GetItems();
            return parameterValues != null
                ? parameterValues.Select(p => new ParameterValueDto(p)).ToList()
                : null;
        }

        public ParameterValueDto UpdateParameterValue(ParameterValueUpdateRequestDto parameterValue)
        {
            var paramValue = _parameterValueRepository.GetItem(parameterValue.Id);
            var techParam = _techParameterRepository.GetItem(parameterValue.TechParameterId);
            if (paramValue == null || techParam == null)
            {
                return null;
            }

            return new ParameterValueDto(_parameterValueRepository.UpdateItem(paramValue));
        }
    }
}
