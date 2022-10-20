using AutoMapper;
using DataAccess;
using DataTransferObjects;
using Entities;

namespace Domain.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IWebServerRepository _webServerRepo;
        public ProductService(IWebServerRepository appServerRepo, IMapper mapper)
        {
            _webServerRepo = appServerRepo ?? throw new ArgumentNullException(nameof(appServerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateProduct(CreateProductDTO productDTO)
        {
            ProductEntity productEntity = _mapper.Map<ProductEntity>(productDTO);
            int rowsAffected = await _webServerRepo.CreateProduct(productEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> UpdateProduct(UpdateProductDTO productDTO)
        {
            ProductEntity productEntity = _mapper.Map<ProductEntity>(productDTO);
            int rowsAffected = await _webServerRepo.UpdateProduct(productEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            int rowsAffected = await _webServerRepo.DeleteProduct(productId);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetProductDTO> GetProduct(Guid productId)
        {
            ProductEntity productEntity = await _webServerRepo.GetProductById(productId);
            if (!string.IsNullOrEmpty(productEntity.Name))
            {
                GetProductDTO productDTO = _mapper.Map<GetProductDTO>(productEntity);
                return productDTO;
            }
            return new GetProductDTO();
        }

        public async Task<List<GetProductDTO>> GetProducts()
        {
            List<ProductEntity> productEntities = await _webServerRepo.GetProducts();
            List<GetProductDTO> productDTOs = new List<GetProductDTO>();
            foreach (ProductEntity productEntity in productEntities)
            {
                productDTOs.Add(_mapper.Map<GetProductDTO>(productEntity));
            }
            return productDTOs;
        }

        public async Task<List<GetAvailableProductDTO>> GetAvailableProducts()
        {
            List<ProductEntity> productEntities = await _webServerRepo.GetAvailableProducts();
            List<GetAvailableProductDTO> productDTOs = new List<GetAvailableProductDTO>();
            foreach (ProductEntity productEntity in productEntities)
            {
                productDTOs.Add(_mapper.Map<GetAvailableProductDTO>(productEntity));
            }
            return productDTOs;
        }
    }
}
