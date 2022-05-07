namespace Application.DTO.Request.PurchaseItem
{
    using System;
    using Application.Interfaces;
    using Domain.Models;

    public abstract class PurchaseItemRequestDto : IDtoMapper<PurchaseItem>
    {
        public Guid ProductId { get; set; }

        public int Count { get; set; }

        public abstract PurchaseItem ToModel();
    }
}
