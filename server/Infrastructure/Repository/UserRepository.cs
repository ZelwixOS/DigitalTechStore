namespace CleanArchitecture.Infra.Data.Repositories
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Infrastructure.Repository;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : BaseRepository, IUserRepository
    {
        private DatabaseContext context;

        public UserRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
            this.context = this.ContextFactory.CreateDbContext(this.ConnectionString);
        }

        public IQueryable<User> GetItems()
        {
            return context.Users.AsNoTracking();
        }

        public User GetItem(Guid id)
        {
            return context.Users.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }
    }
}
