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
            return this.Context.Users
                .Include(u => u.Outlet)
                    .ThenInclude(o => o.City)
                .Include(u => u.Warehouse)
                    .ThenInclude(w => w.City)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public User UpdateUser(User user)
        {
            var entity = this.Context.Users.Update(user);
            this.Context.SaveChanges();
            return entity.Entity;
        }
    }
}
