using System.ComponentModel.DataAnnotations;
using ToDoApplication.Models;

namespace ToDoApplication.ViewModels
{
    public class TaskToDoViewModel
    {
        public int taskId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        public string description { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Category category { get; set; }

        public Status status { get; set; }
        public DateOnly? lastDone { get; set; }
        public List<ShoppingProductViewModel> shoppingProducts { get; set; }
        public string addedBy { get; set; }
    }
}