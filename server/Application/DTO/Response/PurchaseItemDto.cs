namespace Application.DTO.Response
{
    using System;
    using Domain.Models;

    public class PurchaseItemDto
    {
        public PurchaseItemDto()
        {
        }

        public PurchaseItemDto(PurchaseItem purchaseItem)
        {
            this.Id = purchaseItem.Id;
            this.ProductId = purchaseItem.ProductId;
            this.PurchaseId = purchaseItem.PurchaseId;
            this.ProductName = purchaseItem.Product?.Name;
            this.Count = purchaseItem.Count;
            this.Price = purchaseItem.Price;
            this.Sum = purchaseItem.Sum;
        }

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid PurchaseId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { get; set; }
    }
}
