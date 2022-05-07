namespace Application.DTO.Request.Purchase
{
    using System.Collections.Generic;
    using Application.DTO.Request.PurchaseItem;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class PurchaseRequestDto : IDtoMapper<Purchase>
    {
        public string CustomerFullName { get; set; }

        public PaymentType PaymentType { get; set; }

        public string CustomerTelephone { get; set; }

        public int? DeliveryOutletId { get; set; }

        public List<ItemOfPurchaseCreateRequestDto> PurchaseItems { get; set; }

        public abstract Purchase ToModel();
    }
}
