﻿namespace Application.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Domain.Models;

    public class ProductDto : IComparable
    {
        public ProductDto(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Description = product.Description;
            this.Mark = product.Mark;
            this.VendorCode = product.VendorCode;
            this.PicURL = product.PicURL;
            if (product.Category == null)
            {
                this.Category = null;
            }
            else
            {
                this.Category = new CategoryOfProductDto(product.Category);
            }
        }

        public ProductDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public double Mark { get; set; }

        public string VendorCode { get; set; }

        public string PicURL { get; set; }

        public List<ParameterOfProductDto> ProductParameter { get; set; }

        public CategoryOfProductDto Category { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is ProductDto expected)
            {
                if (this.Name == expected.Name && ((this.Category == null && expected.Category == null) || this.Category.CompareTo(expected.Category) == 0) && expected.Price == this.Price && expected.Description == this.Description && expected.Mark == this.Mark && expected.PicURL == this.PicURL && expected.VendorCode == this.VendorCode)
                {
                    if (this.Id == expected.Id)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }

            return -1;
        }
    }
}