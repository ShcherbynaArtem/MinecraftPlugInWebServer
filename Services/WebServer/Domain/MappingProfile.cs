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

            CreateMap<CreateDepartmentDTO, DepartmentEntity>().ReverseMap();
            CreateMap<UpdateDepartmentDTO, DepartmentEntity>().ReverseMap();
            CreateMap<GetDepartmentDTO, DepartmentEntity>().ReverseMap();

            CreateMap<CreateProductTypeDTO, ProductTypeEntity>().ReverseMap();
            CreateMap<UpdateProductTypeDTO, ProductTypeEntity>().ReverseMap();
            CreateMap<GetProductTypeDTO, ProductTypeEntity>().ReverseMap();
        }
    }
}
