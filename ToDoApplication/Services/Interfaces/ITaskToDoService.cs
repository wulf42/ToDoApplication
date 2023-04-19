using ToDoApplication.Models;
using ToDoApplication.ViewModels;

namespace ToDoApplication.Services.Interfaces
{
    public interface ITaskToDoService
    {
        TaskDetailsViewModel Get(int id);

        List<TaskToDo> GetAll();

        int Save(TaskToDo taskToDo);

        int Delete(int id);

        int Edit(int id, TaskDetailsViewModel taskToDo);
    }
}