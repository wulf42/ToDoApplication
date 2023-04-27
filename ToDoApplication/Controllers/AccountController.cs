using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login userLoginData)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginData);
            }

            var result = await _accountService.Login(userLoginData);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userLoginData.UserName);
                HttpContext.Session.SetString("UserId", user.Id);

                return RedirectToAction("Index", "TaskToDo");
            }
            else
            {
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Email not confirmed.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }

                return View(userLoginData);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Register(Register userRegisterData)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterData);
            }

            var result = await _accountService.Register(userRegisterData);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "TaskToDo");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(userRegisterData);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Remove("UserId");
            await _accountService.LogOut();
            return RedirectToAction("Index", "TaskToDo");
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            await _accountService.ConfirmEmail(userId, token);
            return RedirectToAction("Index", "TaskToDo");
        }
    }
}
