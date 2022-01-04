namespace Application.ViewModels
{
    using System;
    using Domain.Models;

    public class ProductOfCategoryDto : IComparable
    {
        public ProductOfCategoryDto(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Description = product.Description;
            this.Mark = product.Mark;
            this.VendorCode = product.VendorCode;
            this.PicURL = product.PicURL;
        }

        public ProductOfCategoryDto()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public double Mark { get; set; }

        public string VendorCode { get; set; }

        public string PicURL { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is ProductOfCategoryDto expected)
            {
                if (this.Name == expected.Name && expected.Price == this.Price && expected.Description == this.Description && expected.Mark == this.Mark && expected.PicURL == this.PicURL && expected.VendorCode == this.VendorCode)
                {
                    if (expected.Id == this.Id)
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
