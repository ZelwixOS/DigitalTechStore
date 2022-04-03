namespace Application.Helpers
{
    using System.Linq;
    using Application.DTO.Request;
    using Domain.Models;

    public class Sorter
    {
        public IQueryable<Product> SortProducts(IQueryable<Product> products, SortingType type, bool reverse)
        {
            if (products != null)
            {
                switch (type)
                {
                    case SortingType.Name: products = products.OrderBy(i => i.Name);  break;
                    case SortingType.Price: products = products.OrderBy(i => i.Price); break;
                    case SortingType.Rating: products = products.OrderBy(i => i.Mark); break;
                    default: break;
                }

                if (reverse)
                {
                    return products.Reverse();
                }
                else
                {
                    return products;
                }
            }

            return products;
        }
    }
}
