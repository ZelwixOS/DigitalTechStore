﻿namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request.Purchase;
    using Application.DTO.Request.PurchaseItem;
    using Application.DTO.Response;
    using Application.DTO.Response.Estate;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository purchsaseRepository;
        private readonly IPurchaseItemRepository purchsaseItemRepository;
        private readonly IDeliveryRepository deliveryRepository;
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly ICityRepository cityRepository;
        private readonly IOutletRepository outletRepository;

        public PurchaseService(
            IPurchaseRepository purchsaseRepository,
            IPurchaseItemRepository purchsaseItemRepository,
            IDeliveryRepository deliveryRepository,
            ICartRepository cartRepository,
            IProductRepository productRepository,
            ICityRepository cityRepository,
            IOutletRepository outletRepository)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.cityRepository = cityRepository;
            this.purchsaseRepository = purchsaseRepository;
            this.deliveryRepository = deliveryRepository;
            this.purchsaseItemRepository = purchsaseItemRepository;
            this.outletRepository = outletRepository;
        }

        public List<PurchaseDto> GetUserPurchases(Guid id)
        {
            var purchase = purchsaseRepository.GetItems().Where(p => p.CustomerId == id).Select(p => new PurchaseDto(p)).ToList();
            return purchase;
        }

        public PurchaseDto GetPurchase(Guid id)
        {
            var purchase = purchsaseRepository.GetItem(id);
            return new PurchaseDto(purchase);
        }

        public PurchaseDto CreatePurchaseByUser(PurchaseCreateRequestDto purchase, User user)
        {
            var purchaseEntity = purchase.ToModel();

            if (user.OutletId.HasValue)
            {
                purchaseEntity.SellerId = user.Id;
                purchaseEntity.OutletId = user.OutletId.Value;
            }
            else
            {
                purchaseEntity.CustomerId = user.Id;
                purchaseEntity.CustomerFullName = null;
                purchaseEntity.CustomerTelephone = null;
            }

            var entity = CreatePurchase(purchase, purchaseEntity);

            if (user.OutletId.HasValue)
            {
                purchaseEntity.Seller = user;
            }
            else
            {
                purchaseEntity.Customer = user;
            }

            var result = new PurchaseDto(entity);

            var items = purchase.PurchaseItems.Select(k => k.ProductId).ToList();

            var cartItemsToDelete = cartRepository.GetItems().Where(c => c.UserId == user.Id && items.Contains(c.ProductId));

            try
            {
                cartRepository.DeleteItems(cartItemsToDelete);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return result;
        }

        public PurchaseDto CreatePurchaseByGuest(PurchaseCreateRequestDto purchase)
        {
            var purchaseEntity = purchase.ToModel();

            return new PurchaseDto(CreatePurchase(purchase, purchaseEntity));
        }

        public decimal GetDeliveryCost(List<ItemOfPurchaseCreateRequestDto> products)
        {
            decimal deliveryCost = 0;
            Product product;
            foreach (var item in products)
            {
                product = productRepository.GetItem(item.ProductId);
                if (product != null)
                {
                    deliveryCost += product.Category.DeliveryCost * item.Count;
                }
            }

            return deliveryCost;
        }

        public int ChangePurchaseStatus(Guid purchaseId, PurchaseStatus purchaseStatus)
        {
            var purchase = purchsaseRepository.GetItem(purchaseId);
            if (purchase == null)
            {
                return 0;
            }

            purchase.Status = purchaseStatus;
            purchsaseRepository.UpdateItem(purchase);
            return 1;
        }

        public PrepurchaseInfoDto GetPrepurchaseInfo(List<ItemOfPurchaseCreateRequestDto> items, int cityId)
        {
            var result = new PrepurchaseInfoDto();
            this.CheckProductsAvailability(cityId, items, null, out List<PurchaseItem> purchaseItems, out decimal deliveryCost);
            result.PurchaseItems = purchaseItems.Select(p => new PurchaseItemDto(p)).ToList();
            result.DeliveryPrice = deliveryCost;
            result.Outlets = outletRepository.GetItems().Where(o => o.CityId == cityId).Select(o => new OutletDto(o)).ToList();
            result.Sum = purchaseItems.Sum(p => p.Sum);
            return result;
        }

        private Purchase CreatePurchase(PurchaseCreateRequestDto purchase, Purchase purchaseEntity)
        {
            if (purchase.PurchaseItems?.Count < 1)
            {
                return null;
            }

            int cityId;

            if (purchase.DeliveryOutletId.HasValue)
            {
                var outlet = outletRepository.GetItem(purchase.DeliveryOutletId.Value);
                if (outlet == null)
                {
                    return null;
                }

                cityId = outlet.CityId;
            }
            else if (purchase.Delivery != null)
            {
                cityId = purchase.Delivery.CityId;
            }
            else
            {
                return null;
            }

            int availability = this.CheckProductsAvailability(cityId, purchase.PurchaseItems, purchase.DeliveryOutletId, out List<PurchaseItem> purchaseItems, out decimal deliveryCost);
            if (availability == 0)
            {
                return null;
            }

            var prodDict = purchaseItems.ToDictionary(p => p.ProductId, p => p.Product);
            purchaseEntity.PurchaseItems = purchaseItems.Select(p =>
            {
                p.Product = null;
                return p;
            }).ToHashSet();

            if (purchase.Delivery != null)
            {
                var delivery = purchase.Delivery.ToModel();
                delivery.DeliveryCost = deliveryCost;
                purchaseEntity.Delivery = delivery;
            }

            purchaseEntity.Status = purchase.Delivery != null ? PurchaseStatus.WaitsForDelivery : (availability == 3 ? PurchaseStatus.WaitsInOulet : PurchaseStatus.TransportingToOutlet);
            purchaseEntity.TotalCost = deliveryCost + purchaseItems.Sum(i => i.Sum);

            var model = purchsaseRepository.CreateItem(purchaseEntity);
            model.PurchaseItems = model.PurchaseItems.Select(i =>
            {
                i.Product = prodDict[i.ProductId];
                return i;
            }).ToHashSet();

            if (model.DeliveryOutletId.HasValue)
            {
                model.DeliveryOutlet = outletRepository.GetItem(model.DeliveryOutletId.Value);
            }

            return model;
        }

        /// <summary>
        /// Checks products availability in city and region.
        /// </summary>
        /// <returns>
        /// 0 - product is not available;
        /// 1 - product is available in the region;
        /// 2 - product is available in the city;
        /// 3 - product is available in the outlet.
        /// </returns>
        private int CheckProductsAvailability(int cityId, List<ItemOfPurchaseCreateRequestDto> products, int? outletId, out List<PurchaseItem> purchaseItems, out decimal deliveryCost)
        {
            int result = 3;
            Product product;
            PurchaseItem purchaseItem;
            deliveryCost = 0;
            int totalCount = 0;
            var city = cityRepository.GetItem(cityId);
            purchaseItems = new List<PurchaseItem>();

            if (city == null)
            {
                return 0;
            }

            int regionId = city.RegionId;
            foreach (var item in products)
            {
                product = productRepository.GetItem(item.ProductId);
                if (product == null)
                {
                    return 0;
                }

                if (outletId.HasValue)
                {
                    totalCount = product.OutletProducts.Where(o => o.Outlet != null && o.Outlet.Id == outletId).Sum(o => o.Count);
                    if (totalCount < item.Count && result > 2)
                    {
                        result = 2;
                    }
                }

                totalCount = product.OutletProducts.Where(o => o.Outlet != null && o.Outlet.CityId == cityId).Sum(o => o.Count);

                if (totalCount < item.Count)
                {
                    if (result > 1)
                    {
                        result = 1;
                    }

                    totalCount += product.WarehouseProducts.Where(o => o.Warehouse != null && o.Warehouse.City != null && o.Warehouse.City.RegionId == regionId).Sum(o => o.Count);
                    if (totalCount < item.Count)
                    {
                        return 0;
                    }
                }

                purchaseItem = item.ToModel();
                purchaseItem.Price = product.Price;
                purchaseItem.Product = product;
                purchaseItem.Sum = product.Price * item.Count;
                purchaseItems.Add(purchaseItem);
                deliveryCost += product.Category.DeliveryCost * item.Count;
            }

            return result;
        }
    }
}
