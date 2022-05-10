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

        public DbSet<ParameterValue> ParameterValues { get; set; }

        public DbSet<User> StoreUsers { get; set; }

        public DbSet<CommonCategory> CommonCategories { get; set; }

        public DbSet<CategoryParameterBlock> CategoryParameterBlocks { get; set; }

        public DbSet<ParameterBlock> ParameterBlocks { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Outlet> Outlets { get; set; }

        public DbSet<OutletProduct> OutletProducts { get; set; }

        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Wish> Wishes { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Purchase> Purchase { get; set; }

        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<ReservedWarehouse> WarehousesReserved { get; set; }

        public DbSet<ReservedOutlet> OutletsReserved { get; set; }

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
                entity.HasOne(p => p.TechParameter).WithMany(t => t.ProductParameters).HasForeignKey(p => p.ParameterIdFk);
                entity.HasOne(p => p.ParameterValue).WithMany(pv => pv.ProductParameters).HasForeignKey(p => p.ParameterValueIdFk);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<TechParameter>(entity =>
            {
                entity.HasOne(t => t.ParameterBlock).WithMany(c => c.Parameters).HasForeignKey(t => t.ParameterBlockIdFk);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasOne(c => c.CommonCategory).WithMany(c => c.Categories).HasForeignKey(c => c.CommonCategoryIdFk);
                entity.HasMany(c => c.CategoryParameterBlocks).WithOne(pb => pb.Category).HasForeignKey(pb => pb.CategoryIdFk);
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<CategoryParameterBlock>(entity =>
            {
                entity.HasOne(p => p.ParameterBlock).WithMany(c => c.CategoryParameterBlocks).HasForeignKey(t => t.ParameterBlockIdFk);
                entity.HasOne(p => p.Category).WithMany(c => c.CategoryParameterBlocks).HasForeignKey(t => t.CategoryIdFk);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<ParameterValue>(entity =>
            {
                entity.HasOne(p => p.TechParameter).WithMany(tp => tp.ParameterValues).HasForeignKey(t => t.TechParameterIdFk);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<ParameterBlock>().HasKey(c => c.Id);

            modelBuilder.Entity<CommonCategory>().HasKey(c => c.Id);

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

            modelBuilder.Entity<Outlet>(entity =>
            {
                entity.HasOne(p => p.City).WithMany(p => p.Outlets).HasForeignKey(p => p.CityId);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasOne(p => p.City).WithMany(p => p.Warehouses).HasForeignKey(p => p.CityId);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<OutletProduct>(entity =>
            {
                entity.HasOne(p => p.Product).WithMany(p => p.OutletProducts).HasForeignKey(p => p.ProductId);
                entity.HasOne(p => p.Outlet).WithMany(p => p.OutletProducts).HasForeignKey(p => p.UnitId);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<WarehouseProduct>(entity =>
            {
                entity.HasOne(p => p.Product).WithMany(p => p.WarehouseProducts).HasForeignKey(p => p.ProductId);
                entity.HasOne(p => p.Warehouse).WithMany(p => p.WarehouseProducts).HasForeignKey(p => p.UnitId);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Region>().HasKey(p => p.Id);

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasOne(p => p.Region).WithMany(p => p.Cities).HasForeignKey(p => p.RegionId);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasOne(p => p.Outlet).WithMany(o => o.Purchases).HasForeignKey(p => p.OutletId);
                entity.HasOne(p => p.Seller).WithMany(s => s.SoldItems).HasForeignKey(p => p.SellerId);
                entity.HasOne(p => p.Customer).WithMany(c => c.PurchasedItems).HasForeignKey(p => p.CustomerId);
                entity.HasOne(p => p.DeliveryOutlet).WithMany(d => d.DeliveredPurchases).HasForeignKey(p => p.DeliveryOutletId);
                entity.HasOne(p => p.Delivery).WithOne(d => d.Purchase).HasForeignKey<Purchase>(p => p.DeliveryId);
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<PurchaseItem>(entity =>
            {
                entity.HasOne(pi => pi.Purchase).WithMany(p => p.PurchaseItems).HasForeignKey(pi => pi.PurchaseId);
                entity.HasOne(pi => pi.Product).WithMany(p => p.PurchaseItems).HasForeignKey(pi => pi.ProductId);
                entity.HasKey(pi => pi.Id);
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasOne(d => d.Purchase).WithOne(p => p.Delivery).HasForeignKey<Delivery>(d => d.PurchaseId);
                entity.HasOne(d => d.City).WithMany(c => c.Deliveries).HasForeignKey(d => d.CityId);
                entity.HasKey(d => d.Id);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.Outlet).WithMany(o => o.Workers).HasForeignKey(u => u.OutletId);
                entity.HasKey(u => u.Id);
            });

            modelBuilder.Entity<ReservedOutlet>(entity =>
            {
                entity.HasOne(r => r.Outlet).WithMany(o => o.ReservedProducts).HasForeignKey(r => r.OutletId);
                entity.HasOne(r => r.Product).WithMany(p => p.OutletsReserved).HasForeignKey(r => r.ProductId);
                entity.HasOne(r => r.Purchase).WithMany(p => p.OutletsReserved).HasForeignKey(r => r.PurchaseId);
            });

            modelBuilder.Entity<ReservedWarehouse>(entity =>
            {
                entity.HasOne(r => r.Warehouse).WithMany(o => o.ReservedProducts).HasForeignKey(r => r.WarehouseId);
                entity.HasOne(r => r.Product).WithMany(p => p.WarehousesReserved).HasForeignKey(r => r.ProductId);
                entity.HasOne(r => r.Purchase).WithMany(p => p.WarehousesReserved).HasForeignKey(r => r.PurchaseId);
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(i => new { i.RoleId, i.UserId });

            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });

            modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }
    }
}
