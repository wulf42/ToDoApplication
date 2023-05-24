using AutoMapper;
using ToDoApplication.Models;
using ToDoApplication.ViewModels;

namespace ToDoApplication.MappingProfiles
{
    public class ShoppingProductMappingProfile : Profile
    {
        public ShoppingProductMappingProfile()
        {
            CreateMap<ShoppingProduct, ShoppingProductViewModel>();
            CreateMap<ShoppingProductViewModel, ShoppingProduct>();
        }
    }
}