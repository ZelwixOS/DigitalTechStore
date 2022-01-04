namespace Application.DTO.Request
{
    using System.Collections.Generic;

    public class GetCategoryProductsRequest : GetProductsRequest
    {
        public List<string> ParameterFilters { get; set; }
    }
}
