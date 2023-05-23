using AutoMapper;
using ToDoApplication.Models;
using ToDoApplication.ViewModels;

namespace ToDoApplication.MappingProfiles
{
    public class ShoppingProductMappingProfile : Profile
    {
        public ShoppingProductMappingProfile()
        {
            CreateMap<ShoppingProduct, ShoppingProductViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<ShoppingProductViewModel, ShoppingProduct>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}