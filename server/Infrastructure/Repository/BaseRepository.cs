namespace Infrastructure.Repository
{
    using Infrastructure.Interfaces;

    public abstract class BaseRepository
    {
        public BaseRepository(string connectionString, IDatabaseContextFactory contextFactory)
        {
            this.ConnectionString = connectionString;
            this.ContextFactory = contextFactory;
        }

        protected string ConnectionString { get; }

        protected IDatabaseContextFactory ContextFactory { get; }
    }
}
