using AutoMapper;
using ECommerce_API.Core;

namespace ECommerce_API.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDTO>();
            CreateMap<Cart, CartResponseDTO>();
            CreateMap<CartItem, CartItemResponseDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
        }
    }
}
