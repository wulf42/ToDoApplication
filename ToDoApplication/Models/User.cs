using Microsoft.AspNetCore.Identity;

namespace ToDoApplication.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            IsActive = true;
        }
        public bool IsActive { get; set; }
    }
}