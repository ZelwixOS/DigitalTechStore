namespace Application.Helpers.HelperModels
{
    using System.Linq;
    using Domain.Models;

    public class ProductsWithPriceRange
    {
        public ProductsWithPriceRange(IQueryable<Product> products, decimal minPrice, decimal maxPrice)
        {
            Products = products;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public IQueryable<Product> Products { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }
}
