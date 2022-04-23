namespace Application.DTO.Request
{
    using System;
    using System.Collections.Generic;

    public class GetCategoryProductsRequest : GetProductsRequest
    {
        public Dictionary<Guid, string> Filters { get; set; }
    }
}
