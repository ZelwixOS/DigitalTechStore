namespace WebApi.Tests.Mock
{
    using System;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;

    internal class DbInitializer
    {
        private readonly string dbconnection;
        private readonly IDatabaseContextFactory databaseContextFactory;

        public DbInitializer(string dbconnection, IDatabaseContextFactory databaseContextFactory)
        {
            this.databaseContextFactory = databaseContextFactory;
            this.dbconnection = dbconnection;
        }

        public void InitDataBase(Action<DatabaseContext> action)
        {
            var context = this.databaseContextFactory.CreateDbContext(this.dbconnection);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            action(context);
        }

        public void InitDataBaseInMemory(Action<DatabaseContext> action)
        {
            var context = this.databaseContextFactory.CreateDbContext(this.dbconnection);
            context.Products.RemoveRange(context.Products);
            context.Categories.RemoveRange(context.Categories);
            action(context);
        }
    }
}
