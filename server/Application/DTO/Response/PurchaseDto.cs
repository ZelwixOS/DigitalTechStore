namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Response.Estate;
    using Domain.Models;

    public class PurchaseDto
    {
        public PurchaseDto()
        {
        }

        public PurchaseDto(Purchase purchase)
        {
            this.Id = purchase.Id;
            var code = this.Id.ToString();
            this.Code = code.Substring(30).ToUpper();
            this.CustomerId = purchase.CustomerId;
            this.CreatedDate = purchase.CreatedDate;
            this.CustomerName = purchase.CustomerFullName ??
                (purchase.Customer != null ? $"{purchase.Customer?.FirstName} {purchase.Customer?.SecondName} ({purchase.Customer?.UserName})" : null);
            this.SellerId = purchase.SellerId;
            this.SellerName = purchase.Seller != null ? $"{purchase.Seller?.FirstName} {purchase.Seller?.SecondName} ({purchase.Seller?.UserName})" : null;
            this.PaymentType = purchase.PaymentType;
            this.Status = purchase.Status;
            this.TotalCost = purchase.TotalCost;
            this.Delivery = purchase.Delivery != null ? new DeliveryDto(purchase.Delivery) : null;
            this.Outlet = purchase.Outlet != null ? new OutletDto(purchase.Outlet) : null;
            this.DeliveryOutlet = purchase.DeliveryOutlet != null ? new OutletDto(purchase.DeliveryOutlet) : null;
            this.PurchaseItems = purchase.PurchaseItems?.Select(x => new PurchaseItemDto(x)).ToList();
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? SellerId { get; set; }

        public string SellerName { get; set; }

        public string CustomerName { get; set; }

        public PaymentType PaymentType { get; set; }

        public PurchaseStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalCost { get; set; }

        public DeliveryDto Delivery { get; set; }

        public OutletDto Outlet { get; set; }

        public OutletDto DeliveryOutlet { get; set; }

        public List<PurchaseItemDto> PurchaseItems { get; set; }
    }
}
