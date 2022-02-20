namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.Interfaces;
    using Application.ViewModels;
    using Domain.Repository;

    public class TechParameterService : ITechParameterService
    {
        private ITechParameterRepository _techParameterRepository;
        private ICategoryRepository _categoryRepository;

        public TechParameterService(ITechParameterRepository techParameterRepository, ICategoryRepository categoryRepository)
        {
            _techParameterRepository = techParameterRepository;
            _categoryRepository = categoryRepository;
        }

        public List<TechParameterDto> GetTechParameters()
        {
            return _techParameterRepository.GetItems().Select(x => new TechParameterDto(x)).ToList();
        }

        public TechParameterDto CreateTechParameter(TechParameterCreateRequestDto techParam)
        {
            return new TechParameterDto(_techParameterRepository.CreateItem(techParam.ToModel()));
        }

        public TechParameterDto UpdatetTechParameter(TechParameterUpdateRequestDto techParam)
        {
            return new TechParameterDto(_techParameterRepository.UpdateItem(techParam.ToModel()));
        }

        public int DeleteTechParameter(Guid id)
        {
            var techParam = _techParameterRepository.GetItem(id);
            if (techParam != null && techParam.ProductParameters.Count == 0)
            {
                return _techParameterRepository.DeleteItem(techParam);
            }
            else
            {
                return 0;
            }
        }
    }
}
