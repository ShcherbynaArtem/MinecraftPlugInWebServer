using AutoMapper;
using DataAccess.ProductRepository;
using DataTransferObjects.ProductDTOs;
using Entities;

namespace Domain.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateProduct(CreateProductDTO productDTO)
        {
            ProductEntity productEntity = _mapper.Map<ProductEntity>(productDTO);
            int rowsAffected = await _productRepository.CreateProduct(productEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> UpdateProduct(UpdateProductDTO productDTO)
        {
            ProductEntity productEntity = _mapper.Map<ProductEntity>(productDTO);
            int rowsAffected = await _productRepository.UpdateProduct(productEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            int rowsAffected = await _productRepository.DeleteProduct(productId);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetProductDTO> GetProduct(Guid productId)
        {
            ProductEntity productEntity = await _productRepository.GetProductById(productId);
            if (!string.IsNullOrEmpty(productEntity.Name))
            {
                GetProductDTO productDTO = _mapper.Map<GetProductDTO>(productEntity);
                return productDTO;
            }
            return new GetProductDTO();
        }

        public async Task<List<GetProductDTO>> GetProducts()
        {
            List<ProductEntity> productEntities = await _productRepository.GetProducts();
            List<GetProductDTO> productDTOs = new List<GetProductDTO>();
            foreach (ProductEntity productEntity in productEntities)
            {
                productDTOs.Add(_mapper.Map<GetProductDTO>(productEntity));
            }
            return productDTOs;
        }

        public async Task<List<GetAvailableProductDTO>> GetAvailableProducts()
        {
            List<ProductEntity> productEntities = await _productRepository.GetAvailableProducts();
            List<GetAvailableProductDTO> productDTOs = new List<GetAvailableProductDTO>();
            foreach (ProductEntity productEntity in productEntities)
            {
                productDTOs.Add(_mapper.Map<GetAvailableProductDTO>(productEntity));
            }
            return productDTOs;
        }
    }
}
