using EmailApp.Model;
using EmailApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;
using ToDoApplication.ViewModels;

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
                    var callbackUrl = $"https://localhost:44300/Account/ConfirmEmail?userId={newUser.Id}&token={encodedToken}";
                    Mail confirmEmailMail = new Mail();
                    confirmEmailMail.from = "ToDoApp@localhost.com";
                    confirmEmailMail.to = userRegisterData.Email;
                    confirmEmailMail.subject = "ToDoApp Email Confirmation";
                    confirmEmailMail.message = $"Please confirm your email by clicking on the link below: \n<a href='{callbackUrl}'>Confirm Mail</a>";

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

        public async Task<bool> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = Uri.EscapeDataString(token);
                var callbackUrl = $"https://localhost:44300/Account/ResetPassword?email={email}&token={encodedToken}";

                Mail resetPasswordMail = new Mail();
                resetPasswordMail.from = "ToDoApp@localhost.com";
                resetPasswordMail.to = user.Email;
                resetPasswordMail.subject = "ToDoApp Password Reset";
                resetPasswordMail.message = $"Please reset your password by clicking on the link below: <br><a href='{callbackUrl}'>Reset Password</a>";
                try
                {
                    //_emailService.SendEmail(resetPasswordMail);
                }
                catch (SmtpException)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<IdentityResult> ResetPassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var token = model.Token;
            var decodedToken = Uri.UnescapeDataString(token);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);
            return result;
        }
    }
}