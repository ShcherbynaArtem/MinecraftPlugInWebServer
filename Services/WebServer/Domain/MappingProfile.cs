using AutoMapper;
using DataTransferObjects;
using Entities;

namespace Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<ProductDTO, ProductEntity>().ReverseMap();
            CreateMap<BundleDTO, BundleEntity>().ReverseMap();
            CreateMap<BundleProductDTO, BundleProductEntity>().ReverseMap();
        }
    }
}
