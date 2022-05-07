namespace Application.DTO.Request.Delivery
{
    using System;
    using Domain.Models;

    public class DeliveryUpdateRequestDto : DeliveryRequestDto
    {
        public Guid Id { get; set; }

        public override Delivery ToModel()
        {
            return new Delivery()
            {
                Id = Id,
                DeliveryAdress = DeliveryAdress,
                RecieverName = RecieverName,
            };
        }
    }
}
