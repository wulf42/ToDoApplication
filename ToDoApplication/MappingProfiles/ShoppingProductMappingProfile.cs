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
                .ForMember(dest => dest.productId, opt => opt.MapFrom(src => src.productId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.quantity));

            CreateMap<ShoppingProductViewModel, ShoppingProduct>()
                .ForMember(dest => dest.productId, opt => opt.MapFrom(src => src.productId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.quantity));
        }
    }
}