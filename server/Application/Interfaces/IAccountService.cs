namespace Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO.Request.Account;
    using Application.DTO.Response;
    using Application.DTO.Response.Account;
    using Domain.Models;
    using Microsoft.AspNetCore.Http;

    public interface IAccountService
    {
        public Task<MessageResultDto> Login(LogInDto model);

        public Task<string> LogOut();

        public Task<MessageResultDto> RegisterViaGoogle(CustomerRegistrationDto model);

        public Task<MessageResultDto> Register(CustomerRegistrationDto model);

        public Task<User> GetCurrentUserAsync(HttpContext httpCont);

        public Task<IList<string>> GetRole(HttpContext httpCont);

        public Task<IList<User>> GetByRole(string role);

        public Task<UserInfo> GetCurrentUserInfo(HttpContext httpCont);

        public Task<MessageResultDto> GoogleAuth(string token);

        public Task<MessageResultDto> Register(WorkerRegistrationDto model);

        public Task<List<WorkerInfo>> GetWorkersAsync(HttpContext httpContext);

        public int BanUser(Guid id);

        public int UnBanUser(Guid id);

        public Task<WorkerInfo> GetWorker(Guid id);

        WorkerInfo UpdateWorker(WorkerUpdateDto model);

        Task<int> UpdateWorkerRoleAsync(Guid id, string role);

        Task<List<UserInfo>> GetClientsAsync();
    }
}
