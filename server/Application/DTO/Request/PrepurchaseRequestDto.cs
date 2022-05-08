namespace Application.DTO.Request
{
    using System.Collections.Generic;
    using Application.DTO.Request.PurchaseItem;

    public class PrepurchaseRequestDto
    {
        public int CityId { get; set; }

        public List<ItemOfPurchaseCreateRequestDto> PurchaseItems { get; set; }
    }
}
