using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public class ShoppingProduct
    {
        [Key]
        public int productId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }

        [ForeignKey("TaskToDo")]
        public int TaskToDoId { get; set; }
        public TaskToDo TaskToDo { get; set; }
    }
}
