namespace Application.DTO.Request.Purchase
{
    using System;
    using System.Linq;
    using Application.DTO.Request.Delivery;
    using Domain.Models;

    public class PurchaseCreateRequestDto : PurchaseRequestDto
    {
        public DeliveryCreateRequestDto Delivery { get; set; }

        public override Purchase ToModel()
        {
            return new Purchase()
            {
                CreatedDate = DateTime.Now,
                CustomerFullName = this.CustomerFullName,
                CustomerTelephone = this.CustomerTelephone,
                DeliveryOutletId = this.DeliveryOutletId,
                PaymentType = this.PaymentType,
                PurchaseItems = this.PurchaseItems?.Select(i => i.ToModel()).ToHashSet(),
            };
        }
    }
}
