namespace Application.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.Helpers.HelperModels;
    using Domain.Models;
    using Microsoft.AspNetCore.Http;

    public class Filter
    {
        public ProductsWithPriceRange FilterByPrice(IQueryable<Product> products, decimal minPrice, decimal maxPrice)
        {
            if (products != null)
            {
                if (maxPrice > 0)
                {
                    products = products.Where(i => i.Price >= minPrice && i.Price <= maxPrice);
                }
                else
                {
                    products = products.Where(i => i.Price >= minPrice);
                }

                decimal min = 0;
                decimal max = 0;

                if (products.Count() != 0)
                {
                    min = products.Min(i => i.Price);
                    max = products.Max(i => i.Price);
                }

                return new ProductsWithPriceRange(products, min, max);
            }

            return new ProductsWithPriceRange(null, 0, 0);
        }

        public IQueryable<Product> FilterByParameters(
            IQueryable<Product> products,
            Dictionary<Guid, List<double>> range,
            Dictionary<Guid, List<Guid>> listValue)
        {
            foreach (var param in range)
            {
                products = products.Where(
                    p => p.ProductParameters.Any(
                        par => par.ParameterIdFk == param.Key &&
                        (param.Value[0] <= par.Value || param.Value[0] <= 0) &&
                        (param.Value[1] >= par.Value || param.Value[1] <= 0)));
            }

            foreach (var param in listValue)
            {
                products = products.Where(
                    p => p.ProductParameters.Any(
                        par => par.ParameterIdFk == param.Key && par.ParameterValueIdFk.HasValue && param.Value.Contains(par.ParameterValueIdFk.Value)));
            }

            return products;
        }

        public (Dictionary<Guid, List<double>> Range, Dictionary<Guid, List<Guid>> ListValue) ConvertParameters(IQueryCollection queryCollection)
        {
            var range = new Dictionary<Guid, List<double>>();
            var listValue = new Dictionary<Guid, List<Guid>>();

            Guid key;
            Guid value;
            double numValue;
            List<string> values;

            foreach (var param in queryCollection)
            {
                if (Guid.TryParse(param.Key, out key))
                {
                    values = param.Value.First()?.Split(",")?.ToList();
                    if (values != null && values.Count > 0)
                    {
                        if (Guid.TryParse(values.First(), out value))
                        {
                            listValue[key] = values.Select(s => Guid.Parse(s)).ToList();
                        }
                        else if (double.TryParse(values.First(), out numValue) && values.Count == 2)
                        {
                            range[key] = values.Select(s => double.Parse(s)).ToList();
                        }
                    }
                }
            }

            return (range, listValue);
        }
    }
}
