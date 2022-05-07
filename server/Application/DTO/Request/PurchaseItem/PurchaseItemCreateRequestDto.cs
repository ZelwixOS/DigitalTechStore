namespace Application.DTO.Request.PurchaseItem
{
    using System;
    using Domain.Models;

    public class PurchaseItemCreateRequestDto : PurchaseItemRequestDto
    {
        public Guid PurchaseId { get; set; }

        public override PurchaseItem ToModel()
        {
            return new PurchaseItem()
            {
                PurchaseId = PurchaseId,
                ProductId = this.ProductId,
                Count = this.Count,
            };
        }
    }
}
