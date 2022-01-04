namespace Infrastructure.Interfaces
{
    using Infrastructure.EF;

    public interface IDatabaseContextFactory
    {
        public DatabaseContext CreateDbContext(string connectionString);
    }
}
