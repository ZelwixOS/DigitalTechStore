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

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Wish> Wishes { get; set; }

        public DbSet<Review> Reviews { get; set; }

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

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasOne(c => c.User).WithMany(u => u.CartItems).HasForeignKey(c => c.UserId);
                entity.HasOne(c => c.Product).WithMany(u => u.CartItems).HasForeignKey(c => c.ProductId);
            });

            modelBuilder.Entity<Wish>(entity =>
            {
                entity.HasOne(w => w.User).WithMany(u => u.WishedItems).HasForeignKey(w => w.UserId);
                entity.HasOne(w => w.Product).WithMany(u => u.WishedItems).HasForeignKey(w => w.ProductId);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(p => p.Product).WithMany(p => p.Reviews).HasForeignKey(p => p.ProductId);
                entity.HasOne(p => p.User).WithMany(t => t.Reviews).HasForeignKey(p => p.UserId);
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
