using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public class Login
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}