using Entities;

namespace DataAccess.BundleProductRepository
{
    public interface IBundleProductRepository
    {
        Task<int> AddProductsToBundle(List<BundleProductEntity> bundleProductEntities);
        Task<int> DeleteProductsFromBundle(Guid id);
    }
}
