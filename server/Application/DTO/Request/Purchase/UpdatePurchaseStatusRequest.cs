namespace Application.DTO.Request.Purchase
{
    using System;
    using Domain.Models;

    public class UpdatePurchaseStatusRequest
    {
        public Guid Id { get; set; }

        public PurchaseStatus Status { get; set; }
    }
}
