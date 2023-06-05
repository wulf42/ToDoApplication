using AutoMapper;
using ToDoApplication.Models;
using ToDoApplication.ViewModels;

namespace ToDoApplication.MappingProfiles
{
    public class TaskToDoMappingProfile : Profile
    {
        public TaskToDoMappingProfile()
        {
            CreateMap<TaskToDo, TaskToDoViewModel>();
            CreateMap<TaskToDoViewModel, TaskToDo>();
        }
    }
}