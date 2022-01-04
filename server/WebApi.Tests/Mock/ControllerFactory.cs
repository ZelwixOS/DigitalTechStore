namespace WebApi.Tests.Mock
{
    using Infrastructure.Interfaces;
    using Microsoft.Extensions.Logging;
    using WebApi.Controllers;

    internal class ControllerFactory
    {
        private readonly ServiceFactory serviceFactory;
        private readonly string dbstring;
        private readonly ProductHelperFactory productHelperFactory;

        public ControllerFactory(string dbstring, IDatabaseContextFactory databaseContextFactory)
        {
            this.dbstring = dbstring;
            this.productHelperFactory = new ProductHelperFactory();
            this.serviceFactory = new ServiceFactory(databaseContextFactory, productHelperFactory);
        }

        public ProductsController CreateProductController()
        {
            return new ProductsController(this.GetLogger<ProductsController>(), this.serviceFactory.CreateProductService(dbstring));
        }

        public CategoryController CreateCategoryController()
        {
            return new CategoryController(this.GetLogger<CategoryController>(), this.serviceFactory.CreateCategoryService(dbstring));
        }

        private ILogger<T> GetLogger<T>()
        {
            return new LoggerFactory().CreateLogger<T>();
        }
    }
}
