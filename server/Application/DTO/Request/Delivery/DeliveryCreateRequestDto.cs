namespace Application.DTO.Request.Delivery
{
    using System;
    using Domain.Models;

    public class DeliveryCreateRequestDto : DeliveryRequestDto
    {
        public Guid PurchaseId { get; set; }

        public int CityId { get; set; }

        public override Delivery ToModel()
        {
            return new Delivery()
            {
                DeliveryAdress = DeliveryAdress,
                PurchaseId = PurchaseId,
                RecieverName = RecieverName,
                CityId = CityId,
            };
        }
    }
}
