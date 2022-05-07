namespace Domain.Models
{
    using System;

    public class Delivery
    {
        public Guid Id { get; set; }

        public Guid? PurchaseId { get; set; }

        public string RecieverName { get; set; }

        public int CityId { get; set; }

        public string DeliveryAdress { get; set; }

        public decimal DeliveryCost { get; set; }

        public Purchase Purchase { get; set; }

        public City City { get; set; }
    }
}
