namespace Application.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Helpers.HelperModels;
    using Domain.Models;

    public class Paginator
    {
        public ProductsWithLastPage ElementsOfPage(IQueryable<Product> products, int currentPage, int itemsOnPage)
        {
            if (products != null && itemsOnPage > 0)
            {
                int pageTotalNumber = MaxPageCount(products, itemsOnPage);

                if (pageTotalNumber >= currentPage && currentPage > 0)
                {
                    int finish = currentPage * itemsOnPage;
                    var prod = products.Take(finish).Skip(finish - itemsOnPage).ToList();

                    return new ProductsWithLastPage(prod, pageTotalNumber);
                }
                else
                {
                    return new ProductsWithLastPage(new List<Product>(), pageTotalNumber);
                }
            }
            else
            {
                return new ProductsWithLastPage(new List<Product>(), 0);
            }
        }

        public int MaxPageCount(IQueryable<Product> products, int itemsOnPage)
        {
            int prodNum = products.Count();
            int pageTotalNumber = prodNum / itemsOnPage;
            if (prodNum % itemsOnPage > 0)
            {
                pageTotalNumber++;
            }

            return pageTotalNumber;
        }
    }
}
