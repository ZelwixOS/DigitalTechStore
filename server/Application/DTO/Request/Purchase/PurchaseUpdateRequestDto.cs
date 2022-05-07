namespace Application.DTO.Request.Purchase
{
    using System;
    using System.Linq;
    using Domain.Models;

    public class PurchaseUpdateRequestDto : PurchaseRequestDto
    {
        public Guid Id { get; set; }

        public PurchaseStatus Status { get; set; }

        public override Purchase ToModel()
        {
            return new Purchase()
            {
                Id = this.Id,
                CustomerFullName = this.CustomerFullName,
                CustomerTelephone = this.CustomerTelephone,
                DeliveryOutletId = this.DeliveryOutletId,
                PaymentType = this.PaymentType,
                Status = this.Status,
                PurchaseItems = this.PurchaseItems?.Select(i =>
                {
                    var item = i.ToModel();
                    item.PurchaseId = this.Id;
                    return item;
                }).ToHashSet(),
            };
        }
    }
}
