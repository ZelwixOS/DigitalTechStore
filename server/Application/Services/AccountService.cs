namespace Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO.Request.Account;
    using Application.DTO.Response.Account;
    using Application.Helpers;
    using Application.Interfaces;
    using Domain.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<MessageResultDto> Login(LogInDto model)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return new MessageResultDto("Logged in as: " + model.Login, null);
            }
            else
            {
                List<string> err = new List<string>();
                err.Add("Wrong login or password");

                return new MessageResultDto("Sorry, can't log in.", err);
            }
        }

        public async Task<string> LogOut()
        {
            await this.signInManager.SignOutAsync();
            var msg = "Log out completed";

            return msg;
        }

        public async Task<MessageResultDto> Register(CustomerRegistrationDto model)
        {
            User user = new User
            {
                Email = model.Email,
                UserName = model.Login,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, RoleManager.Customer);

                await this.signInManager.SignInAsync(user, false);

                var msg = "New customer was registered: " + user.UserName;

                return new MessageResultDto(msg, null);
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }

                return new MessageResultDto("Error. Customer wasn't registered.", errorList);
            }
        }

        public Task<User> GetCurrentUserAsync(HttpContext httpCont) => this.userManager.GetUserAsync(httpCont.User);

        public async Task<IList<string>> GetRole(HttpContext httpCont)
        {
            var usr = await this.userManager.GetUserAsync(httpCont.User);
            if (usr != null)
            {
                return await this.userManager.GetRolesAsync(usr);
            }
            else
            {
                var rolesList = new List<string>();
                rolesList.Add("Guest");
                return rolesList;
            }
        }

        public Task<IList<User>> GetByRole(string role)
        {
            return this.userManager.GetUsersInRoleAsync(role);
        }

        public async Task<string> GetCurrentUserInfo(HttpContext httpCont)
        {
            User usr = await GetCurrentUserAsync(httpCont);
            var message = usr == null ? string.Empty : usr.UserName;
            return message;
        }
    }
}
