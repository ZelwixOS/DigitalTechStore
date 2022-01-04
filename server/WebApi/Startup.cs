namespace WebApi
{
    using System;
    using System.Threading.Tasks;
    using Application.Helpers;
    using Application.Interfaces;
    using Application.Services;
    using CleanArchitecture.Infra.Data.Repositories;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Interfaces;
    using Microsoft.AspNetCore.Authentication.Cookies;
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
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

            services.AddScoped<IDatabaseContextFactory, DatabaseContextFactory>();
            services.AddScoped<ICategoryRepository, CategoryRepository>(provider => new CategoryRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IProductRepository, ProductRepository>(provider => new ProductRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<IProductParameterRepository, ProductParameterRepository>(provider => new ProductParameterRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));
            services.AddScoped<ITechParameterRepository, TechParameterRepository>(provider => new TechParameterRepository(dbconectionString, provider.GetService<IDatabaseContextFactory>()));

            services.AddSingleton<ProductHelpersContainer>();

            services.AddScoped<IProductService, ProductService>(provider => new ProductService(provider.GetService<IProductRepository>(), provider.GetService<ICategoryRepository>(), provider.GetService<ProductHelpersContainer>()));
            services.AddScoped<ICategoryService, CategoryService>(provider => new CategoryService(provider.GetService<ICategoryRepository>(), provider.GetService<IProductRepository>(), provider.GetService<ProductHelpersContainer>()));
            services.AddScoped<IProductParameterService, ProductParameterService>(provider => new ProductParameterService(provider.GetService<IProductParameterRepository>(), provider.GetService<IProductRepository>(), provider.GetService<ITechParameterRepository>()));
            services.AddScoped<ITechParameterService, TechParameterService>(provider => new TechParameterService(provider.GetService<ITechParameterRepository>(), provider.GetService<ICategoryRepository>()));
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
