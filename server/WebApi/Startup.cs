namespace WebApi
{
    using System;
    using System.Threading.Tasks;
    using Application.Helpers;
    using Application.Interfaces;
    using Application.Services;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Infrastructure.Repository;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.Google;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string dbconectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(options => options.UseSqlServer(dbconectionString));

            services.AddControllersWithViews();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<DatabaseContext>();

            services.AddSingleton<ProductHelpersContainer>();
            services.AddScoped<RoleManager<IdentityRole<Guid>>>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRolesIntializer, RolesInitializer>();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                o.DefaultForbidScheme = GoogleDefaults.AuthenticationScheme;
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
             .AddCookie()
             .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
             {
                 IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                 options.ClientId = googleAuthNSection["ClientId"];
                 options.ClientSecret = googleAuthNSection["ClientSecret"];
             });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "DNStoreCookies";
                options.LoginPath = "/User/Login/";
                options.AccessDeniedPath = "/User/AccessDenied/";
                options.LogoutPath = "/User/Logout/";
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.AddSingleton<IDatabaseContextFactory, DatabaseContextFactory>();
            services.AddScoped<ICategoryRepository, CategoryRepository>(provider =>
                new CategoryRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IProductRepository, ProductRepository>(provider =>
                new ProductRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IProductParameterRepository, ProductParameterRepository>(provider =>
                new ProductParameterRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<ITechParameterRepository, TechParameterRepository>(provider =>
                new TechParameterRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IParameterBlockRepository, ParameterBlockRepository>(provider =>
                new ParameterBlockRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IUserRepository, UserRepository>(provider =>
                new UserRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<ICommonCategoryRepository, CommonCategoryRepository>(provider =>
                new CommonCategoryRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<ICartRepository, CartRepository>(provider =>
                new CartRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IWishRepository, WishRepository>(provider =>
                new WishRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IReviewRepository, ReviewRepository>(provider =>
                new ReviewRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<ICategoryParameterBlockRepository, CategoryParameterBlockRepository>(provider =>
                new CategoryParameterBlockRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IParameterValueRepository, ParameterValueRepository>(provider =>
                new ParameterValueRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IRegionRepository, RegionRepository>(provider =>
                new RegionRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));
            services.AddScoped<ICityRepository, CityRepository>(provider =>
                new CityRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));
            services.AddScoped<IOutletRepository, OutletRepository>(provider =>
                new OutletRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));
            services.AddScoped<IWarehouseRepository, WarehouseRepository>(provider =>
                new WarehouseRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));
            services.AddScoped<IOutletProductRepository, OutletProductRepository>(provider =>
                new OutletProductRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));
            services.AddScoped<IWarehouseProductRepository, WarehouseProductRepository>(provider =>
                new WarehouseProductRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));
            services.AddScoped<IPurchaseRepository, PurchaseRepository>(provider =>
                new PurchaseRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));
            services.AddScoped<IPurchaseItemRepository, PurchaseItemRepository>(provider =>
                new PurchaseItemRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));
            services.AddScoped<IDeliveryRepository, DeliveryRepository>(provider =>
                new DeliveryRepository(dbconectionString, provider.GetRequiredService<IDatabaseContextFactory>()));

            services.AddSingleton<ProductHelpersContainer>();

            services.AddScoped<IProductService, ProductService>(provider =>
                new ProductService(
                    provider.GetService<IProductRepository>(),
                    provider.GetService<ICategoryRepository>(),
                    provider.GetService<ProductHelpersContainer>()));
            services.AddScoped<ICategoryService, CategoryService>(provider =>
                new CategoryService(
                    provider.GetService<ICategoryRepository>(),
                    provider.GetService<IProductRepository>(),
                    provider.GetService<ICommonCategoryRepository>(),
                    provider.GetService<ProductHelpersContainer>()));
            services.AddScoped<IProductParameterService, ProductParameterService>(provider =>
                new ProductParameterService(
                    provider.GetService<IProductParameterRepository>(),
                    provider.GetService<IProductRepository>(),
                    provider.GetService<ITechParameterRepository>()));
            services.AddScoped<ITechParameterService, TechParameterService>(provider =>
                new TechParameterService(
                    provider.GetService<ITechParameterRepository>(),
                    provider.GetService<IParameterBlockRepository>(),
                    provider.GetService<ICategoryParameterBlockRepository>()));
            services.AddScoped<ICommonCategoryService, CommonCategoryService>(provider =>
                new CommonCategoryService(provider.GetService<ICommonCategoryRepository>()));
            services.AddScoped<ICustomerListsService, CustomerListsService>(provider =>
                new CustomerListsService(
                    provider.GetService<ICartRepository>(),
                    provider.GetService<IWishRepository>(),
                    provider.GetService<IProductRepository>()));
            services.AddScoped<IReviewService, ReviewService>(provider =>
                new ReviewService(
                    provider.GetService<IReviewRepository>(),
                    provider.GetService<IUserRepository>(),
                    provider.GetService<IProductRepository>()));
            services.AddScoped<IParameterValueService, ParameterValueService>(provider =>
                new ParameterValueService(provider.GetService<IParameterValueRepository>(), provider.GetService<ITechParameterRepository>()));
            services.AddScoped<IGeographyService, GeographyService>(provider =>
                new GeographyService(provider.GetService<IRegionRepository>(), provider.GetService<ICityRepository>()));
            services.AddScoped<IEstateService, EstateService>(provider =>
                new EstateService(
                    provider.GetService<IOutletRepository>(),
                    provider.GetService<IWarehouseRepository>(),
                    provider.GetService<IOutletProductRepository>(),
                    provider.GetService<IWarehouseProductRepository>()));
            services.AddScoped<IPurchaseService, PurchaseService>(provider =>
                new PurchaseService(
                    provider.GetService<IPurchaseRepository>(),
                    provider.GetService<IPurchaseItemRepository>(),
                    provider.GetService<IDeliveryRepository>(),
                    provider.GetService<ICartRepository>(),
                    provider.GetService<IProductRepository>(),
                    provider.GetService<ICityRepository>(),
                    provider.GetService<IOutletRepository>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, IRolesIntializer rolesInit)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });

            rolesInit.CreateUserRoles().Wait();
        }
    }
}
