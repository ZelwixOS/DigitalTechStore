namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.DTO.Request.ParameterBlock;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class TechParameterService : ITechParameterService
    {
        private ITechParameterRepository _techParameterRepository;
        private IParameterBlockRepository _parameterBlockRepository;
        private ICategoryParameterBlockRepository _categoryParameterBlockRepository;

        public TechParameterService(
            ITechParameterRepository techParameterRepository,
            IParameterBlockRepository parameterBlockRepository,
            ICategoryParameterBlockRepository categoryParameterBlockRepository)
        {
            _techParameterRepository = techParameterRepository;
            _parameterBlockRepository = parameterBlockRepository;
            _categoryParameterBlockRepository = categoryParameterBlockRepository;
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

        public List<ParameterBlockDto> GetParameterBlocks()
        {
            return _parameterBlockRepository.GetItems().Select(p => new ParameterBlockDto(p, false)).ToList();
        }

        public ParameterBlockDto CreateParameterBlock(ParameterBlockCreateRequestDto block)
        {
            return new ParameterBlockDto(_parameterBlockRepository.CreateItem(block.ToModel()));
        }

        public ParameterBlockDto UpdateParameterBlock(ParameterBlockUpdateRequestDto block)
        {
            return new ParameterBlockDto(_parameterBlockRepository.UpdateItem(block.ToModel()));
        }

        public int LinkCategoryParameterBlock(Guid id, Guid categoryId)
        {
            var categoryParameterBlock = new CategoryParameterBlock()
            {
                CategoryIdFk = categoryId,
                ParameterBlockIdFk = id,
                Important = false,
            };

            return _categoryParameterBlockRepository.CreateItem(categoryParameterBlock) != null ? 1 : 0;
        }

        public int SetCategoryParameterBlocks(List<CategoryParameterBlockRequestDto> blocks, Guid categoryId)
        {
            var toDelete = _categoryParameterBlockRepository.GetItems().Where(cb => cb.CategoryIdFk == categoryId);
            _categoryParameterBlockRepository.DeletItems(toDelete);
            var items = blocks.Select(b => b.ToModel(categoryId)).ToList();
            return _categoryParameterBlockRepository.CreateItems(items);
        }

        public int UnlinkCategoryParameterBlock(Guid id, Guid categoryId)
        {
            var categoryParameterBlock = _categoryParameterBlockRepository.GetItem(id);
            if (categoryParameterBlock != null)
            {
                return 0;
            }

            return _categoryParameterBlockRepository.DeleteItem(categoryParameterBlock);
        }

        public int SetBlockImportantStatus(Guid id, bool status)
        {
            var categoryParameterBlock = _categoryParameterBlockRepository.GetItem(id);
            if (categoryParameterBlock == null)
            {
                return 0;
            }

            categoryParameterBlock.Important = status;
            return _categoryParameterBlockRepository.UpdateItem(categoryParameterBlock) != null ? 1 : 0;
        }

        public int DeleteParameterBlock(Guid id)
        {
            var block = _parameterBlockRepository.GetItem(id);
            if (block != null && block.Parameters.Count == 0 && block.CategoryParameterBlocks.Count == 0)
            {
                return _parameterBlockRepository.DeleteItem(block);
            }
            else
            {
                return 0;
            }
        }
    }
}
