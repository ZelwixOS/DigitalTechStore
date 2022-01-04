namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.DTO.Request.Account;
    using Application.DTO.Response.Account;
    using Application.Helpers;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using static Google.Apis.Auth.GoogleJsonWebSignature;
    using Answer = Application.Helpers.Constants.AnswerMessage;
    using GoogleCode = Application.Helpers.Constants.GoogleAuthResultCodes;

    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration config;
        private readonly IUserRepository userRepository;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
            this.userRepository = userRepository;
        }

        public async Task<MessageResultDto> Login(LogInDto model)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, true);
            if (result.Succeeded)
            {
                return new MessageResultDto(Answer.LoggedAs + model.Login, null, Constants.AnswerCodes.SignedIn);
            }
            else
            {
                List<string> err = new List<string>();
                err.Add(Answer.WrongCreds);

                return new MessageResultDto(Answer.LoginError, err);
            }
        }

        public async Task<MessageResultDto> GoogleAuth(string token)
        {
            try
            {
                var gUser = await GetGoogleUser(token);
                switch (await CheckGoogleUser(gUser))
                {
                    case GoogleCode.UserFound:
                        var dbUser = userRepository.GetItems().Where(u => u.GoogleMail == gUser.Email).FirstOrDefault();
                        await signInManager.SignInAsync(dbUser, true);
                        return new MessageResultDto(Answer.LoggedAs + dbUser.UserName, null, Constants.AnswerCodes.SignedIn);
                    case GoogleCode.NoUserInDB:
                        return new MessageResultDto(Answer.Redirection, null, Constants.AnswerCodes.GoToGoogleRegistrationPage);
                    case GoogleCode.EmailNotConnectedWithAccount:
                        return new MessageResultDto(Answer.LoginError, new List<string> { Answer.NotConnectedGoogle });
                    default: return null;
                }
            }
            catch (Exception err)
            {
                return new MessageResultDto(Answer.LoginError, new List<string> { err.Message });
            }
        }

        public async Task<string> LogOut()
        {
            await this.signInManager.SignOutAsync();
            return Answer.LogOutSucceed;
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
                await this.userManager.AddToRoleAsync(user, Constants.RoleManager.Customer);

                await this.signInManager.SignInAsync(user, false);

                var msg = Answer.RegisteredSuccessfully + user.UserName;

                return new MessageResultDto(msg, null);
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }

                return new MessageResultDto(Answer.RegisteredUnsuccessfully, errorList);
            }
        }

        public async Task<MessageResultDto> RegisterViaGoogle(CustomerRegistrationDto model)
        {
            User user = new User
            {
                Email = model.Email,
                UserName = model.Login,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                PhoneNumber = model.PhoneNumber,
            };

            try
            {
                var gUser = await GetGoogleUser(model.Token);
                if (await CheckGoogleUser(gUser) == GoogleCode.NoUserInDB)
                {
                    user.GoogleMail = gUser.Email;
                }
                else
                {
                    return new MessageResultDto(Answer.RegisteredUnsuccessfully, null);
                }
            }
            catch
            {
                return new MessageResultDto(Answer.RegisteredUnsuccessfully, null);
            }

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, Constants.RoleManager.Customer);

                await this.signInManager.SignInAsync(user, false);

                var msg = Answer.RegisteredSuccessfully + user.UserName;

                return new MessageResultDto(msg, null);
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }

                return new MessageResultDto(Answer.RegisteredUnsuccessfully, errorList);
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
                rolesList.Add(Constants.RoleManager.Guest);
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

        private async Task<Payload> GetGoogleUser(string token)
        {
            IConfigurationSection googleAuthNSection = config.GetSection("Authentication:Google");

            return await ValidateAsync(token, new ValidationSettings()
            {
                Audience = new[] { googleAuthNSection["ClientId"] },
            });
        }

        private async Task<GoogleCode> CheckGoogleUser(Payload gUser)
        {
            User dbUser = userRepository.GetItems().Where(u => u.GoogleMail == gUser.Email).FirstOrDefault();
            if (dbUser != null)
            {
                return GoogleCode.UserFound;
            }
            else
            {
                dbUser = await userManager.FindByEmailAsync(gUser.Email);
                return (dbUser != null) ? GoogleCode.EmailNotConnectedWithAccount : GoogleCode.NoUserInDB;
            }
        }
    }
}
