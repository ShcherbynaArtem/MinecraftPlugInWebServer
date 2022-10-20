using Entities;

namespace DataAccess.ProductTypeRepository
{
    public interface IProductTypeRepository
    {
        Task<int> CreateProductType(ProductTypeEntity productTypeEntity);
        Task<int> UpdateProductType(ProductTypeEntity productTypeEntity);
        Task<int> DeleteProductType(int idproductTypeId);
        Task<ProductTypeEntity> GetProductTypeById(int productTypeId);
        Task<List<ProductTypeEntity>> GetProductTypes();
    }
}
