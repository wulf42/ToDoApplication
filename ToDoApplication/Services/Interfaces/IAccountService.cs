using Microsoft.AspNetCore.Identity;
using ToDoApplication.Models;

namespace ToDoApplication.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> Login(Login userLoginData);
        Task<IdentityResult> Register(Register userRegisterData);
        Task LogOut();
    }
}
