using AutoMapper;
using DataAccess.ProductTypeRepository;
using DataTransferObjects.ProductTypeDTOs;
using Entities;

namespace Domain.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IMapper _mapper;
        private readonly IProductTypeRepository _productTypeRepository;
        public ProductTypeService(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateProductType(CreateProductTypeDTO productTypeDTO)
        {
            ProductTypeEntity productTypeEntity = _mapper.Map<ProductTypeEntity>(productTypeDTO);
            int rowsAffected = await _productTypeRepository.CreateProductType(productTypeEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> UpdateProductType(UpdateProductTypeDTO productTypeDTO)
        {
            ProductTypeEntity productTypeEntity = _mapper.Map<ProductTypeEntity>(productTypeDTO);
            int rowsAffected = await _productTypeRepository.UpdateProductType(productTypeEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteProductType(int productTypeId)
        {
            int rowsAffected = await _productTypeRepository.DeleteProductType(productTypeId);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetProductTypeDTO> GetProductType(int productTypeId)
        {
            ProductTypeEntity productTypeEntity = await _productTypeRepository.GetProductTypeById(productTypeId);
            if (!string.IsNullOrEmpty(productTypeEntity.Name))
            {
                GetProductTypeDTO productTypeDTO = _mapper.Map<GetProductTypeDTO>(productTypeEntity);
                return productTypeDTO;
            }
            return new GetProductTypeDTO();
        }

        public async Task<List<GetProductTypeDTO>> GetProductTypes()
        {
            List<ProductTypeEntity> productTypeEntities = await _productTypeRepository.GetProductTypes();
            List<GetProductTypeDTO> productTypeDTOs = new List<GetProductTypeDTO>();
            foreach (ProductTypeEntity productTypeEntity in productTypeEntities)
            {
                productTypeDTOs.Add(_mapper.Map<GetProductTypeDTO>(productTypeEntity));
            }
            return productTypeDTOs;
        }
    }
}
