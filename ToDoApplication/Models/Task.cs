namespace ToDoApplication.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }


    }

    public enum Category
    {
        DeepWork, ShallowWork, Chores, Learning, MindCare, BodyCare, People
    }
    public enum Status
    {
        ToDo, InProgress, Done
    }
}
