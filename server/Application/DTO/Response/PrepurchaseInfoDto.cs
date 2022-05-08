namespace Application.DTO.Response
{
    using System.Collections.Generic;
    using Application.DTO.Response.Estate;

    public class PrepurchaseInfoDto
    {
        public List<PurchaseItemDto> PurchaseItems { get; set; }

        public List<OutletDto> Outlets { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal Sum { get; set; }
    }
}
