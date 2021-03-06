namespace Infrastructure.EF
{
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        public DatabaseContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
