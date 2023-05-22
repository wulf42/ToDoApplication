using AutoMapper;
using ToDoApplication.Models;
using ToDoApplication.ViewModels;

namespace ToDoApplication.MappingProfiles
{
    public class TaskToDoMappingProfile : Profile
    {
        public TaskToDoMappingProfile()
        {
            CreateMap<TaskToDo, TaskToDoViewModel>()
                .ForMember(dest => dest.taskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.lastDone, opt => opt.MapFrom(src => src.LastDone));

            CreateMap<TaskToDoViewModel, TaskToDo>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.taskId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.date))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.time))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.category))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.LastDone, opt => opt.MapFrom(src => src.lastDone));
        }
    }
}