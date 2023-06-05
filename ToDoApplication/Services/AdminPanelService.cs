using Microsoft.AspNetCore.Identity;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;
using ToDoApplication.ViewModels;

namespace ToDoApplication.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly UserManager<User> _userManager;

        public AdminPanelService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> EditUsers(List<UserRolesViewModel> model)
        {
            foreach (var userRole in model)
            {
                var user = await _userManager.FindByIdAsync(userRole.User.Id);

                if (user == null)
                {
                    return false;
                }

                var isEmailEdited = await EditEmail(user, userRole.User.Email);
                var isEmailConfirmedEdited = await EditEmailConfirmed(user, userRole.User.EmailConfirmed);
                var isIsActiveEdited = await EditIsActive(user, userRole.User.IsActive);
                var isRoleEdited = await EditRole(user, userRole.Role);

                if (!isEmailEdited || !isEmailConfirmedEdited || !isIsActiveEdited || !isRoleEdited)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> EditEmail(User user, string email)
        {
            if (user.Email != email)
            {
                user.Email = email;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }

            return true;
        }

        private async Task<bool> EditEmailConfirmed(User user, bool emailConfirmed)
        {
            if (user.EmailConfirmed != emailConfirmed)
            {
                user.EmailConfirmed = emailConfirmed;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }

            return true;
        }

        private async Task<bool> EditIsActive(User user, bool isActive)
        {
            if (user.IsActive != isActive)
            {
                user.IsActive = isActive;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }

            return true;
        }

        private async Task<bool> EditRole(User user, string role)
        {
            var existingRoles = await _userManager.GetRolesAsync(user);

            if (!existingRoles.Contains(role))
            {
                await _userManager.RemoveFromRolesAsync(user, existingRoles);
                await _userManager.AddToRoleAsync(user, role);
            }

            return true;
        }
    }
}