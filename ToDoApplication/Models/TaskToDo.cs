using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApplication.Models
{
    public class TaskToDo
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        [Required]
        public Category Category { get; set; }

        public Status Status { get; set; }
        public List<ShoppingProduct>? shoppingLists { get; set; }
        public string? addedBy { get; set; }
    }

    public enum Category
    {
        DeepWork, ShallowWork, Chores, Learning, MindCare, BodyCare, People, ShoppingList, Other
    }

    public enum Status
    {
        ToDo, InProgress, Done
    }


}