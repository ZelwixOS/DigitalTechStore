namespace Application.DTO.Request.Delivery
{
    using Application.Interfaces;
    using Domain.Models;

    public abstract class DeliveryRequestDto : IDtoMapper<Delivery>
    {
        public string RecieverName { get; set; }

        public string DeliveryAdress { get; set; }

        public abstract Delivery ToModel();
    }
}
