using AutoMapper;
using DataAccess;
using DataTransferObjects;
using Entities;

namespace Domain.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IMapper _mapper;
        private readonly IWebServerRepository _webServerRepo;
        public ProductTypeService(IWebServerRepository appServerRepo, IMapper mapper)
        {
            _webServerRepo = appServerRepo ?? throw new ArgumentNullException(nameof(appServerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateProductType(CreateProductTypeDTO productTypeDTO)
        {
            ProductTypeEntity productTypeEntity = _mapper.Map<ProductTypeEntity>(productTypeDTO);
            int rowsAffected = await _webServerRepo.CreateProductType(productTypeEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> UpdateProductType(UpdateProductTypeDTO productTypeDTO)
        {
            ProductTypeEntity productTypeEntity = _mapper.Map<ProductTypeEntity>(productTypeDTO);
            int rowsAffected = await _webServerRepo.UpdateProductType(productTypeEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteProductType(int productTypeId)
        {
            int rowsAffected = await _webServerRepo.DeleteProductType(productTypeId);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetProductTypeDTO> GetProductType(int productTypeId)
        {
            ProductTypeEntity productTypeEntity = await _webServerRepo.GetProductTypeById(productTypeId);
            if (!string.IsNullOrEmpty(productTypeEntity.Name))
            {
                GetProductTypeDTO productTypeDTO = _mapper.Map<GetProductTypeDTO>(productTypeEntity);
                return productTypeDTO;
            }
            return new GetProductTypeDTO();
        }

        public async Task<List<GetProductTypeDTO>> GetProductTypes()
        {
            List<ProductTypeEntity> productTypeEntities = await _webServerRepo.GetProductTypes();
            List<GetProductTypeDTO> productTypeDTOs = new List<GetProductTypeDTO>();
            foreach (ProductTypeEntity productTypeEntity in productTypeEntities)
            {
                productTypeDTOs.Add(_mapper.Map<GetProductTypeDTO>(productTypeEntity));
            }
            return productTypeDTOs;
        }
    }
}
