namespace WebApi.Tests.Mock
{
    using Application.Services;
    using Infrastructure.Interfaces;

    internal class ServiceFactory
    {
        private readonly RepositoryFactory repositoryFactory;
        private readonly ProductHelperFactory productHelperFactory;

        public ServiceFactory(IDatabaseContextFactory repositoryContextFactory, ProductHelperFactory productHelperFactory)
        {
            this.repositoryFactory = new RepositoryFactory(repositoryContextFactory);
            this.productHelperFactory = productHelperFactory;
        }

        public ProductService CreateProductService(string dbstring)
        {
            return new ProductService(repositoryFactory.CreateProductRepository(dbstring), repositoryFactory.CreateCategoryRepository(dbstring), productHelperFactory.GetHelper());
        }

        public CategoryService CreateCategoryService(string dbstring)
        {
            return new CategoryService(repositoryFactory.CreateCategoryRepository(dbstring), productHelperFactory.GetHelper());
        }
    }
}
