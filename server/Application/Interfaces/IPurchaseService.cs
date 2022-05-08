namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request.Purchase;
    using Application.DTO.Request.PurchaseItem;
    using Application.DTO.Response;
    using Domain.Models;

    public interface IPurchaseService
    {
        int ChangePurchaseStatus(Guid purchaseId, PurchaseStatus purchaseStatus);

        PurchaseDto CreatePurchaseByGuest(PurchaseCreateRequestDto purchase);

        PurchaseDto CreatePurchaseByUser(PurchaseCreateRequestDto purchase, User user);

        decimal GetDeliveryCost(List<ItemOfPurchaseCreateRequestDto> products);

        PrepurchaseInfoDto GetPrepurchaseInfo(List<ItemOfPurchaseCreateRequestDto> items, int cityId);

        PurchaseDto GetPurchase(Guid id);

        List<PurchaseDto> GetUserPurchases(Guid id);
    }
}
