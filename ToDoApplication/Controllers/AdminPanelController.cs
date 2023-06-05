using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.ViewModels;

namespace ToDoApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    public class ManageUsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ManageUsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            var model = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

                var userRolesViewModel = new UserRolesViewModel
                {
                    User = user,
                    Role = role
                };

                model.Add(userRolesViewModel);
            }

            return View(model);
        }
    }
}