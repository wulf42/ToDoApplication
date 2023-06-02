namespace ToDoApplication.Services.Interfaces
{
    public interface IRoleInitializationService
    {
        Task InitializeRolesAsync(string[] roles);
    }
}
