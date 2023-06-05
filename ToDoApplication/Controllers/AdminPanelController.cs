using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;
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
    [Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {
        private readonly IAdminPanelService _adminPanelService;
        private readonly UserManager<User> _userManager;

        public ManageUsersController(IAdminPanelService adminPanelService, UserManager<User> userManager)
        {
            _adminPanelService = adminPanelService;
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

        [HttpPost]
        public async Task<IActionResult> Edit(List<UserRolesViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _adminPanelService.EditUsers(model);

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error occurred while editing users.");
                return View(model);
            }
        }
    }
}