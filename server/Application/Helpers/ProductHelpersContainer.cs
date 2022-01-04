namespace Application.Helpers
{
    public class ProductHelpersContainer
    {
        public ProductHelpersContainer()
        {
            Paginator = new Paginator();
            Sorter = new Sorter();
            Filter = new Filter();
        }

        public Paginator Paginator { get; }

        public Sorter Sorter { get; }

        public Filter Filter { get; }
    }
}
