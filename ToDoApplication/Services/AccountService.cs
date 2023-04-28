using EmailApp.Model;
using EmailApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Web;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<SignInResult> Login(Login userLoginData)
        {
            var userName = userLoginData.UserName;
            var password = userLoginData.Password;

            var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    result = SignInResult.NotAllowed;
                }
            }

            return result;
        }
        public async Task<IdentityResult> Register(Register userRegisterData)
        {
            var newUser = new User
            {
                UserName = userRegisterData.UserName,
                Email = userRegisterData.Email,
            };




            var result = await _userManager.CreateAsync(newUser, userRegisterData.Password);

            if (result.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string encodedToken = Uri.EscapeDataString(token);

                if (!string.IsNullOrEmpty(token))
                {
                    Mail confirmEmailMail = new Mail();
                    confirmEmailMail.from = "ToDoApp@localhost.com";
                    confirmEmailMail.to = userRegisterData.Email;
                    confirmEmailMail.subject = "ToDoApp Email Confirmation";
                    confirmEmailMail.message = "Please confirm your email by clicking on the link below: \n" +
                                               "https://localhost:44300/Account/ConfirmEmail?userId=" +
                                               HttpUtility.UrlEncode(newUser.Id) +
                                               "&token=" + encodedToken;

                    //Send email confirmation
                    _emailService.SendEmail(confirmEmailMail);
                }

            }

            return result;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> ConfirmEmail(string userId, string token)
        {
            string decodedToken = Uri.UnescapeDataString(token);
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            return result;
        }
    }
}
