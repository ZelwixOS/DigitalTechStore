namespace WebApi.Tests.Mock
{
    using System.Data.Common;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    internal class DatabaseContextFactoryMock : IDatabaseContextFactory
    {
        private DbConnection connection;

        public DbContextOptions<DatabaseContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(this.connection).Options;
        }

        public DatabaseContext CreateDbContext(string connectionString)
        {
            if (this.connection == null)
            {
                this.connection = new SqliteConnection(connectionString);
                this.connection.Open();

                var options = this.CreateOptions();
                using (var context = new DatabaseContext(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new DatabaseContext(this.CreateOptions());
        }

        public void Dispose()
        {
            if (this.connection != null)
            {
                this.connection.Dispose();
                this.connection = null;
            }
        }
    }
}
