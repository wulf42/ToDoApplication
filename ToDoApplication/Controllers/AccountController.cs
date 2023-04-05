using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;

namespace ToDoApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            var result = await _signInManager.PasswordSignInAsync(userLoginData.UserName, userLoginData.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userLoginData.UserName);
                var userId = user.Id;
                HttpContext.Session.SetString("UserId", userId);

                return RedirectToAction("Index", "TaskToDo");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
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

            var newUser = new User
            {
                UserName = userRegisterData.UserName,
                Email = userRegisterData.Email,
            };

            await _userManager.CreateAsync(newUser, userRegisterData.Password);
            return RedirectToAction("Index", "TaskToDo");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "TaskToDo");
        }
    }
}
