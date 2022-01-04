namespace CleanArchitecture.Infra.Data.Repositories
{
    using System;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private DatabaseContext context;

        public UserRepository(DatabaseContext context)
        {
            this.context = context;
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
