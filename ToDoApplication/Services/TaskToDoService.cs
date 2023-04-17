using ToDoApplication.Context;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;
using ToDoApplication.ViewModels;

namespace ToDoApplication.Services
{
    public class TaskToDoService : ITaskToDoService
    {
        private readonly ToDoApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaskToDoService(ToDoApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public TaskDetailsViewModel Get(int id)
        {
            var taskToDo = _context.TasksToDo.Find(id);
            var shoppingProducts = _context.ShoppingProducts
            .Where(x => x.TaskToDoId == id)
            .ToList();


            var viewModel = new TaskDetailsViewModel
            {
                TaskToDo = taskToDo,
                ShoppingProducts = shoppingProducts
            };


            return viewModel;
        }

        public List<TaskToDo> GetAll()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            var tasksToDoList = _context.TasksToDo.Where(t => t.addedBy == userId).ToList();


            return tasksToDoList;
        }

        public int Save(TaskToDo taskToDo)
        {
            _context.TasksToDo.Add(taskToDo);
            _context.SaveChanges();

            return taskToDo.TaskId;
        }

        public int Delete(int id)
        {
            var taskToDo = _context.TasksToDo.Find(id);
            _context.TasksToDo.Remove(taskToDo);
            _context.SaveChanges();
            return taskToDo.TaskId;
        }

        public int Edit(int id, TaskDetailsViewModel body)
        {
            var taskToUpdate = _context.TasksToDo.Find(id);
            //Edit task form data
            if (body.TaskToDo.TaskId != 0)
            {
                taskToUpdate.Name = body.TaskToDo.Name;
                taskToUpdate.Description = body.TaskToDo.Description;
                taskToUpdate.Category = body.TaskToDo.Category;
                //taskToUpdate.Status = body.Status;
                taskToUpdate.Date = body.TaskToDo.Date;
                taskToUpdate.Time = body.TaskToDo.Time;
            }
            //Move to next category (passed only int id, TaskToDo body is empty)
            if (body.TaskToDo.TaskId == 0)
            {
                taskToUpdate.Status = body.TaskToDo.Status;
            }

            foreach (var shoppingProduct in body.ShoppingProducts)
            {
                Edit(shoppingProduct.productId, shoppingProduct);
            }

            _context.TasksToDo.Update(taskToUpdate);
            _context.SaveChanges();
            return taskToUpdate.TaskId;
        }

        public int Edit(int shoppingProductId, ShoppingProduct body)
        {
            //funkcja edytująca produkt z listy zakupów
            var shoppingProduct = _context.ShoppingProducts.Find(shoppingProductId);
 
            shoppingProduct.name = body.name;
            shoppingProduct.quantity = body.quantity;

            _context.SaveChanges();
            return shoppingProduct.productId;
        }




    }
}