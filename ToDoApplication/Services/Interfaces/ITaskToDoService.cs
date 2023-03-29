using ToDoApplication.Models;

namespace ToDoApplication.Services.Interfaces
{

    public interface ITaskToDoService
    {
        TaskToDo Get(int id);
        List<TaskToDo> GetAll();
        int Save(TaskToDo taskToDo);
        int Delete(int id);
    }
}
