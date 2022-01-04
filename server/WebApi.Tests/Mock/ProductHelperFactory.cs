namespace WebApi.Tests.Mock
{
    using Application.Helpers;

    internal class ProductHelperFactory
    {
        private ProductHelpersContainer _helper;

        public ProductHelperFactory()
        {
            _helper = new ProductHelpersContainer();
        }

        public ProductHelpersContainer GetHelper()
        {
            return _helper;
        }
    }
}
