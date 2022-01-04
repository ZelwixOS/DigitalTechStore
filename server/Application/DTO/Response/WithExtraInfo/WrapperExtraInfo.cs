namespace Application.DTO.Response.WithExtraInfo
{
    public class WrapperExtraInfo<T>
        where T : class
    {
        public WrapperExtraInfo(T data, int maxPage, decimal minPrice, decimal maxPrice)
        {
            Container = data;
            MaxPage = maxPage;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public T Container { get; set; }

        public int MaxPage { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }
}
