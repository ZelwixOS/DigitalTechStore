namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.DTO.Request.Account;
    using Application.DTO.Response;
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
    using Role = Application.Helpers.Constants.RoleManager;

    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration config;
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly IWishRepository wishRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IOutletRepository outletRepository;

        public AccountService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config,
            IUserRepository userRepository,
            ICartRepository cartRepository,
            IWishRepository wishRepository,
            IWarehouseRepository warehouseRepository,
            IOutletRepository outletRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
            this.userRepository = userRepository;
            this.cartRepository = cartRepository;
            this.wishRepository = wishRepository;
            this.warehouseRepository = warehouseRepository;
            this.outletRepository = outletRepository;
        }

        public async Task<MessageResultDto> Login(LogInDto model)
        {
            var user = this.userRepository.GetItems().FirstOrDefault(u => u.UserName == model.Login && !u.Banned);
            if (user == null)
            {
                return new MessageResultDto(Answer.LoginError, new List<string> { "Пользователь не найден" });
            }

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
                        var dbUser = userRepository.GetItems().Where(u => u.GoogleMail == gUser.Email && !u.Banned).FirstOrDefault();
                        if (dbUser == null)
                        {
                            new MessageResultDto(Answer.LoginError, new List<string> { "Пользователь не найден." });
                        }

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

        public async Task<List<WorkerInfo>> GetWorkersAsync(HttpContext httpContext)
        {
            var curUser = (await this.GetRole(httpContext)).FirstOrDefault();
            List<string> rolesToGet;
            switch (curUser)
            {
                case Role.Admin:
                    rolesToGet = new List<string> { Role.Courier, Role.Manager, Role.ShopAssistant, Role.WarehouseWorker };
                    break;
                case Role.Manager:
                    rolesToGet = new List<string> { Role.Courier, Role.ShopAssistant };
                    break;
                default:
                    rolesToGet = new List<string>();
                    break;
            }

            var workers = new List<WorkerInfo>();
            var outlets = outletRepository.GetItems();
            var warehouses = warehouseRepository.GetItems();

            IList<User> users;
            foreach (string role in rolesToGet)
            {
                users = await userManager.GetUsersInRoleAsync(role);
                workers.AddRange(users.Select(u =>
                {
                    if (u.OutletId.HasValue)
                    {
                        u.Outlet = outlets.FirstOrDefault(o => o.Id == u.OutletId.Value);
                    }
                    else
                    {
                        u.Warehouse = warehouses.FirstOrDefault(w => w.Id == u.WarehouseId.Value);
                    }

                    return new WorkerInfo(u, role);
                }));
            }

            return workers;
        }

        public async Task<List<UserInfo>> GetClientsAsync()
        {
            var clients = new List<UserInfo>();

            IList<User> users = await this.userManager.GetUsersInRoleAsync(Role.Customer);
            clients = users.Select(u => new UserInfo(u)).ToList();

            return clients;
        }

        public async Task<WorkerInfo> GetWorker(Guid id)
        {
            var user = this.userRepository.GetItem(id);
            if (user == null)
            {
                return null;
            }

            var role = (await this.userManager.GetRolesAsync(user)).FirstOrDefault();

            return new WorkerInfo(user, role);
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
                Avatar = "defaultAvatar",
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, Role.Customer);

                await this.signInManager.SignInAsync(user, false);

                var msg = Answer.RegisteredSuccessfully + user.UserName;

                return new MessageResultDto(msg, null, Constants.AnswerCodes.SignedIn);
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

        public async Task<MessageResultDto> Register(WorkerRegistrationDto model)
        {
            var roleName = model.RoleName;
            if (roleName != Role.Courier && roleName != Role.Manager && roleName != Role.ShopAssistant && roleName != Role.WarehouseWorker)
            {
                return new MessageResultDto(Answer.RegisteredWorkerUnSuccessfully, new List<string> { "Выбрана неверная роль." });
            }

            if (!model.WarehouseId.HasValue && !model.OutletId.HasValue)
            {
                return new MessageResultDto(Answer.RegisteredWorkerUnSuccessfully, new List<string> { "Не выбран магазин или склад." });
            }
            else
            {
                Outlet outlet = null;
                Warehouse warehouse = null;
                if (model.OutletId.HasValue)
                {
                    outlet = outletRepository.GetItem(model.OutletId.Value);
                }

                if (model.WarehouseId.HasValue)
                {
                    warehouse = warehouseRepository.GetItem(model.WarehouseId.Value);
                }

                if (outlet == null && warehouse == null)
                {
                    return new MessageResultDto(Answer.RegisteredUnsuccessfully, new List<string> { "Выбран неверный магазин или склад." });
                }
                else
                {
                    if (outlet != null)
                    {
                        model.WarehouseId = null;
                    }
                    else
                    {
                        model.OutletId = null;
                    }
                }
            }

            User user = new User
            {
                Email = model.Email,
                UserName = model.Login,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                PhoneNumber = model.PhoneNumber,
                OutletId = model.OutletId,
                WarehouseId = model.WarehouseId,
                Avatar = "defaultAvatar",
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, model.RoleName);

                var msg = Answer.RegisteredWorkerSuccessfully + user.UserName;
                return new MessageResultDto(msg, null, Constants.AnswerCodes.SignedIn);
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }

                return new MessageResultDto(Answer.RegisteredWorkerUnSuccessfully, errorList);
            }
        }

        public WorkerInfo UpdateWorker(WorkerUpdateDto model)
        {
            if (!model.WarehouseId.HasValue && !model.OutletId.HasValue)
            {
                return null;
            }
            else
            {
                Outlet outlet = null;
                Warehouse warehouse = null;
                if (model.OutletId.HasValue)
                {
                    outlet = outletRepository.GetItem(model.OutletId.Value);
                }

                if (model.WarehouseId.HasValue)
                {
                    warehouse = warehouseRepository.GetItem(model.WarehouseId.Value);
                }

                if (outlet == null && warehouse == null)
                {
                    return null;
                }
                else
                {
                    if (outlet != null)
                    {
                        model.WarehouseId = null;
                    }
                    else
                    {
                        model.OutletId = null;
                    }
                }
            }

            var user = this.userRepository.GetItem(model.Id);
            if (user == null)
            {
                return null;
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.SecondName = model.SecondName;
            user.PhoneNumber = model.PhoneNumber;
            user.OutletId = model.OutletId;
            user.WarehouseId = model.WarehouseId;
            var res = this.userRepository.UpdateUser(user);
            if (res == null)
            {
                return null;
            }

            return new WorkerInfo(user, string.Empty);
        }

        public async Task<int> UpdateWorkerRoleAsync(Guid id, string role)
        {
            if (role != Role.Courier && role != Role.Manager && role != Role.ShopAssistant && role != Role.WarehouseWorker)
            {
                return 0;
            }

            var user = this.userRepository.GetItem(id);
            var curRoles = await this.userManager.GetRolesAsync(user);
            var rolesAddRes = await this.userManager.AddToRoleAsync(user, role);
            var rolesRemovingRes = await this.userManager.RemoveFromRolesAsync(user, curRoles);

            if (rolesAddRes.Succeeded && rolesRemovingRes.Succeeded)
            {
                return 1;
            }

            return 0;
        }

        public int BanUser(Guid id)
        {
            var user = this.userRepository.GetItem(id);
            if (user == null)
            {
                return 0;
            }

            user.Banned = true;

            var res = this.userRepository.UpdateUser(user);

            return res == null ? 0 : 1;
        }

        public int UnBanUser(Guid id)
        {
            var user = this.userRepository.GetItem(id);
            if (user == null)
            {
                return 0;
            }

            user.Banned = false;

            var res = this.userRepository.UpdateUser(user);

            return res == null ? 0 : 1;
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
                    user.Avatar = gUser.Picture;
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
                await this.userManager.AddToRoleAsync(user, Role.Customer);

                await this.signInManager.SignInAsync(user, false);

                var msg = Answer.RegisteredSuccessfully + user.UserName;

                return new MessageResultDto(msg, null, Constants.AnswerCodes.SignedIn);
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

        public async Task<UserInfo> GetCurrentUserInfo(HttpContext httpCont)
        {
            User usr = await GetCurrentUserAsync(httpCont);
            if (usr != null)
            {
                string role = (await this.userManager.GetRolesAsync(usr)).FirstOrDefault();
                if (role != null)
                {
                    var userInfo = new UserInfo(usr, role);
                    userInfo.CartCount = cartRepository.GetItemsByUser(usr.Id).Count();
                    userInfo.WishListCount = wishRepository.GetItemsByUser(usr.Id).Count();
                    return userInfo;
                }
            }

            return null;
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
