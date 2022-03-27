namespace Application.DTO.Response
{
    using System;
    using Domain.Models;

    public class TechParameterDto
    {
        public TechParameterDto(TechParameter techParam)
        {
            this.Id = techParam.Id;
            this.Name = techParam.Name;
            this.Important = techParam.Important;
            this.CategoryId = techParam.CategoryIdFk;
        }

        public TechParameterDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Important { get; set; }

        public Guid CategoryId { get; set; }
    }
}
