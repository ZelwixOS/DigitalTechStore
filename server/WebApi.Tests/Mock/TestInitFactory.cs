namespace WebApi.Tests.Mock
{
    using Infrastructure.Interfaces;

    internal class TestInitFactory
    {
        private readonly IDatabaseContextFactory databaseContextFactory = new DatabaseContextFactoryMock();
        private readonly IDatabaseContextFactory databaseInMemoryContextFactory = new DatabaseContextFactoryInMemoryMock();

        private readonly string dbConnection = "DataSource=:memory:";
        private readonly string dbName = "InMemoryDB";

        public ControllerFactory CreateControllerFactory()
        {
            return new ControllerFactory(this.dbConnection, databaseContextFactory);
        }

        public DbInitializer CreateDbInitalizer()
        {
            return new DbInitializer(this.dbConnection, databaseContextFactory);
        }

        public ControllerFactory CreateControllerInMemoryFactory()
        {
            return new ControllerFactory(this.dbName, databaseInMemoryContextFactory);
        }

        public DbInitializer CreateDbInitalizerInMemory()
        {
            return new DbInitializer(this.dbName, databaseInMemoryContextFactory);
        }
    }
}
