namespace Application.Helpers.HelperModels
{
    using System.Collections.Generic;
    using Domain.Models;

    public class ProductsWithLastPage
    {
        public ProductsWithLastPage(List<Product> products, int maxPage)
        {
            Products = products;
            MaxPage = maxPage;
        }

        public List<Product> Products { get; set; }

        public int MaxPage { get; set; }
    }
}
