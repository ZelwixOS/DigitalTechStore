namespace Application.DTO.Request
{
    public class GetProductsRequest
    {
        public int PageNumber { get; set; }

        public int ItemsOnPage { get; set; }

        public SortingType SortingType { get; set; }

        public bool ReverseSorting { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }
}
