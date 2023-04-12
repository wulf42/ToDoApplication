using ToDoApplication.Context;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

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

        public TaskToDo Get(int id)
        {
            var taskToDo = _context.TasksToDo.Find(id);

            return taskToDo;
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

        public int Edit(int id, TaskToDo body)
        {
            var taskToUpdate = _context.TasksToDo.Find(id);
            //Edit task form data
            if (body.TaskId != 0)
            {
                taskToUpdate.Name = body.Name;
                taskToUpdate.Description = body.Description;
                taskToUpdate.Category = body.Category;
                //taskToUpdate.Status = body.Status;
                taskToUpdate.Date = body.Date;
                taskToUpdate.Time = body.Time;
            }
            //Move to next category (passed only int id, TaskToDo body is empty)
            if (body.TaskId == 0)
            {
                taskToUpdate.Status = body.Status;
            }
            _context.TasksToDo.Update(taskToUpdate);
            _context.SaveChanges();
            return taskToUpdate.TaskId;
        }
    }
}