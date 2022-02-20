namespace Application.ViewModels
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
        }

        public TechParameterDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Important { get; set; }
    }
}
