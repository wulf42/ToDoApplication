using System.ComponentModel.DataAnnotations;
using ToDoApplication.Models;

namespace ToDoApplication.ViewModels
{
    public class TaskToDoViewModel
    {
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }

        public Status Status { get; set; }
        public DateOnly? LastDone { get; set; }
        public List<ShoppingProductViewModel> ShoppingProducts { get; set; }
        public string AddedBy { get; set; }
    }
}