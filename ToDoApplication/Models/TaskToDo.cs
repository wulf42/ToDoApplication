using System.ComponentModel.DataAnnotations;

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
    }

    public enum Category
    {
        DeepWork, ShallowWork, Chores, Learning, MindCare, BodyCare, People, Other
    }

    public enum Status
    {
        ToDo, InProgress, Done
    }
}