using Microsoft.AspNetCore.Identity;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class RoleInitializationService : IRoleInitializationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleInitializationService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task InitializeRolesAsync(string[] roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
