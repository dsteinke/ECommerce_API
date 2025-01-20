using AutoMapper;
using Entities;
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
        }
    }
}
