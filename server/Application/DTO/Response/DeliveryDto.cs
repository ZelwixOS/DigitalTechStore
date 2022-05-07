namespace Application.DTO.Response
{
    using System;
    using Domain.Models;

    public class DeliveryDto
    {
        public DeliveryDto()
        {
        }

        public DeliveryDto(Delivery delivery)
        {
            this.Id = delivery.Id;
            this.RecieverName = delivery.RecieverName;
            this.DeliveryAdress = delivery.DeliveryAdress;
            this.DeliveryCost = delivery.DeliveryCost;
        }

        public Guid Id { get; set; }

        public string RecieverName { get; set; }

        public string DeliveryAdress { get; set; }

        public decimal DeliveryCost { get; set; }
    }
}
