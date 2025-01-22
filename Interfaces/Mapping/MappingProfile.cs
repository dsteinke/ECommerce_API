using AutoMapper;
using Entities;
using Interfaces.DTO.Cart;
using Interfaces.DTO.ProductDTO;
using Interfaces.DTO.User;

namespace Interfaces.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDTO>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<Cart, CartResponseDTO>();
            CreateMap<CartItem, CartItemResponseDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
        }
    }
}
