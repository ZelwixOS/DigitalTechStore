namespace WebApi.Tests.Mock
{
    using CleanArchitecture.Infra.Data.Repositories;
    using Domain.Repository;
    using Infrastructure.Interfaces;

    internal class RepositoryFactory
    {
        private readonly IDatabaseContextFactory dbContextFactory;

        public RepositoryFactory(IDatabaseContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public IProductRepository CreateProductRepository(string dbstring)
        {
            return new ProductRepository(dbstring, dbContextFactory);
        }

        public ICategoryRepository CreateCategoryRepository(string dbstring)
        {
            return new CategoryRepository(dbstring, dbContextFactory);
        }
    }
}
