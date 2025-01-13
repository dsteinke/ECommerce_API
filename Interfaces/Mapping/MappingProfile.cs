using AutoMapper;
using Entities;
using Interfaces.DTO.ProductDTO;

namespace Interfaces.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}
