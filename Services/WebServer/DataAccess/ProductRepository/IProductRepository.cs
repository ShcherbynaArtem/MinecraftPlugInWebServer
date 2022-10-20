using Entities;

namespace DataAccess.ProductRepository
{
    public interface IProductRepository
    {
        Task<int> CreateProduct(ProductEntity productEntity);
        Task<int> UpdateProduct(ProductEntity productEntity);
        Task<int> DeleteProduct(Guid productId);
        Task<ProductEntity> GetProductById(Guid productId);
        Task<List<ProductEntity>> GetProducts();
        Task<List<ProductEntity>> GetAvailableProducts();
    }
}
