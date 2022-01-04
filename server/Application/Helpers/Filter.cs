namespace Application.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.Helpers.HelperModels;
    using Domain.Models;

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

        public IQueryable<Product> FilterByParameters(IQueryable<Product> products, HashSet<ProductParameter> prodParameters, Guid categoryId)
        {
            if (products != null)
            {
                if (prodParameters.Count == 0)
                {
                    return products.Where(p => p.CategoryIdFk == categoryId);
                }

                foreach (var param in prodParameters)
                {
                    products = products.Where(p => p.ProductParameters.FirstOrDefault(i => (i.ParameterIdFk == param.ParameterIdFk && i.Value == param.Value)) != null);
                }

                return products;
            }

            return null;
        }

        public HashSet<ProductParameter> ConvertParameters(HashSet<TechParameter> techParameters, List<string> filters)
        {
            var existingParams = new HashSet<ProductParameter>();

            if (techParameters != null && filters != null)
            {
                foreach (var filter in filters)
                {
                    var filterNormal = StringToFiltrationParameter(filter);
                    if (filterNormal != null)
                    {
                        var matchedParam = techParameters.FirstOrDefault(i => i.Name == filterNormal.Name);
                        if (matchedParam != null)
                        {
                            existingParams.Add(new ProductParameter() { ParameterIdFk = matchedParam.Id, Value = filterNormal.Value });
                        }
                    }
                }

                return existingParams;
            }

            return existingParams;
        }

        private FiltrationParameter StringToFiltrationParameter(string filter)
        {
            if (filter != null && filter.Contains('='))
            {
                var parts = filter.Split('=');
                return new FiltrationParameter() { Name = parts[0], Value = parts[1] };
            }
            else
            {
                return null;
            }
        }
    }
}
