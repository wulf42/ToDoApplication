using Microsoft.AspNetCore.Identity;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            return result;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
