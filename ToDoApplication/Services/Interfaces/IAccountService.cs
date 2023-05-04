using Microsoft.AspNetCore.Identity;
using ToDoApplication.Models;
using ToDoApplication.ViewModels;

namespace ToDoApplication.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> Login(Login userLoginData);
        Task<IdentityResult> Register(Register userRegisterData);
        Task LogOut();
        Task<IdentityResult> ConfirmEmail(string userId, string token);
        Task<bool> ForgotPassword(string email);
        Task<IdentityResult> ResetPassword(ChangePasswordViewModel body);
    }
}
