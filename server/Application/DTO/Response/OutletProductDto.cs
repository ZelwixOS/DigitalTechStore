namespace Application.DTO.Response
{
    using System.Linq;
    using Application.DTO.Response.Estate;
    using Domain.Models;

    public class OutletProductDto
    {
        public OutletProductDto(OutletProduct outlet)
        {
            this.Outlet = new OutletDto(outlet.Outlet);
            this.Count = outlet.Count - outlet.Outlet.ReservedProducts.Where(o => o.OutletId == outlet.UnitId).Sum(r => r.Count);
        }

        public OutletProductDto()
        {
        }

        public int Count { get; set; }

        public OutletDto Outlet { get; set; }
    }
}
