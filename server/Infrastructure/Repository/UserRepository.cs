namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, IDatabaseContextFactory contextFactory)
            : base(connectionString, contextFactory)
        {
        }

        public IQueryable<User> GetItems()
        {
            return this.Context.Users.AsNoTracking();
        }

        public User GetItem(Guid id)
        {
            return this.Context.Users.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }
    }
}
