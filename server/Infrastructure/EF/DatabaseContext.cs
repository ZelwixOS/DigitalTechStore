namespace Infrastructure.EF
{
    using System;
    using Domain.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                 : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<TechParameter> TechParameters { get; set; }

        public DbSet<ProductParameter> ProductParameters { get; set; }

        public DbSet<User> StoreUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryIdFk);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<ProductParameter>(entity =>
            {
                entity.HasOne(p => p.Product).WithMany(p => p.ProductParameters).HasForeignKey(p => p.ProductIdFk);
                entity.HasOne(p => p.Parameter).WithMany(t => t.ProductParameters).HasForeignKey(p => p.ParameterIdFk);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<TechParameter>(entity =>
            {
                entity.HasOne(t => t.Category).WithMany(c => c.Parameters).HasForeignKey(t => t.CategoryIdFk);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(i => new { i.RoleId, i.UserId });

            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });

            modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }
    }
}
