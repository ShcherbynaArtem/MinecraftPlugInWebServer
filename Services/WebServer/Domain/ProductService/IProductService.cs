using DataTransferObjects.ProductDTOs;

namespace Domain.ProductService
{
    public interface IProductService
    {
        Task<bool> CreateProduct(CreateProductDTO userDTO);
        Task<bool> UpdateProduct(UpdateProductDTO userDTO);
        Task<bool> DeleteProduct(Guid productId);
        Task<GetProductDTO> GetProduct(Guid productId);
        Task<List<GetProductDTO>> GetProducts();
        Task<List<GetAvailableProductDTO>> GetAvailableProducts();
    }
}
