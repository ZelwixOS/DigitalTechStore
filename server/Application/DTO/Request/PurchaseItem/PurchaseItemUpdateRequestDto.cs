namespace Application.DTO.Request.PurchaseItem
{
    using System;
    using Domain.Models;

    public class PurchaseItemUpdateRequestDto : PurchaseItemRequestDto
    {
        public Guid Id { get; set; }

        public Guid PurchaseId { get; set; }

        public override PurchaseItem ToModel()
        {
            return new PurchaseItem()
            {
                Id = Id,
                PurchaseId = PurchaseId,
                ProductId = this.ProductId,
                Count = this.Count,
            };
        }
    }
}
