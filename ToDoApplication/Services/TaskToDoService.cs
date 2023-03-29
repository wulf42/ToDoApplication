using ToDoApplication.Context;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class TaskToDoService : ITaskToDoService
    {
        private readonly ToDoApplicationDbContext _context;
        public TaskToDoService(ToDoApplicationDbContext context)
        {
            _context = context;
        }

        public TaskToDo Get(int id)
        {
            var taskToDo = _context.TasksToDo.Find(id);

            return taskToDo;
        }
        public List<TaskToDo> GetAll()
        {
            var tasksToDoList = _context.TasksToDo.ToList();

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
            return id;
        }

    }
}
