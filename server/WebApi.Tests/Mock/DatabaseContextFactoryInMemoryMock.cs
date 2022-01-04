namespace WebApi.Tests.Mock
{
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContextFactoryInMemoryMock : IDatabaseContextFactory
    {
        private DatabaseContext context;

        public DatabaseContext CreateDbContext(string connectionString)
        {
            if (context == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseInMemoryDatabase(databaseName: connectionString);
                context = new DatabaseContext(optionsBuilder.Options);
            }

            return context;
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
                this.context = null;
            }
        }
    }
}
