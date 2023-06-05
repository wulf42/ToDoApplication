using System.ComponentModel.DataAnnotations;
using ToDoApplication.Models;

namespace ToDoApplication.ViewModels
{
    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserRolesViewModel
    {
        public User User { get; set; }
        public string Role { get; set; }
    }
}