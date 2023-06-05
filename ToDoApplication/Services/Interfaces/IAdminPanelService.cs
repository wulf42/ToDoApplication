using ToDoApplication.ViewModels;

namespace ToDoApplication.Services.Interfaces
{
    public interface IAdminPanelService
    {
        Task<bool> EditUsers(List<UserRolesViewModel> model);
    }
}
