using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApplication.Models
{
    public class ShoppingProduct
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("TaskToDo")]
        public int TaskToDoId { get; set; }

        public TaskToDo TaskToDo { get; set; }
    }
}