namespace Application.Services
{
    using System;
    using System.Threading.Tasks;
    using Application.Helpers;
    using Application.Interfaces;
    using Domain.Models;
    using Microsoft.AspNetCore.Identity;

    public class RolesInitializer : IRolesIntializer
    {
        private RoleManager<IdentityRole<Guid>> roleManager;

        private UserManager<User> userManager;

        public RolesInitializer(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task CreateUserRoles()
        {
            if (await roleManager.FindByNameAsync(Constants.RoleManager.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Constants.RoleManager.Admin));
            }

            if (await roleManager.FindByNameAsync(Constants.RoleManager.Customer) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Constants.RoleManager.Customer));
            }

            if (await roleManager.FindByNameAsync(Constants.RoleManager.Manager) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Constants.RoleManager.Manager));
            }

            if (await roleManager.FindByNameAsync(Constants.RoleManager.ShopAssistant) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Constants.RoleManager.ShopAssistant));
            }

            if (await roleManager.FindByNameAsync(Constants.RoleManager.WarehouseWorker) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Constants.RoleManager.WarehouseWorker));
            }

            if (await roleManager.FindByNameAsync(Constants.RoleManager.Courier) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Constants.RoleManager.Courier));
            }

            await CreateUserAsync("adminNE@mail.com", "Admin", "Main", "Administrator", "Aa123456!", "89807350000", Constants.RoleManager.Admin);
            await CreateUserAsync("costomerNE@mail.com", "Sergey", "Prohorov", "TestCustomer", "Aa123456!", "89807350000", Constants.RoleManager.Customer);
        }

        private async Task CreateUserAsync(string email, string firstName, string secondName, string login, string password, string phoneNumber, string role)
        {
            if (await userManager.FindByNameAsync(login) == null)
            {
                User newUser = new User
                {
                    UserName = login,
                    Email = email,
                    FirstName = firstName,
                    SecondName = secondName,
                    PhoneNumber = phoneNumber,
                };
                IdentityResult result = await userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, role);
                }
            }
        }
    }
}
