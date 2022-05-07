namespace Application.DTO.Request.PurchaseItem
{
    using Domain.Models;

    public class ItemOfPurchaseCreateRequestDto : PurchaseItemRequestDto
    {
        public override PurchaseItem ToModel()
        {
            return new PurchaseItem()
            {
                ProductId = this.ProductId,
                Count = this.Count,
            };
        }
    }
}
