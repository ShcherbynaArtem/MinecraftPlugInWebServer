using Entities;

namespace DataAccess.BundleRepository
{
    public interface IBundleRepository
    {
        Task<Guid> CreateBundle(BundleEntity bundleEntity);
        Task<int> UpdateBundle(BundleEntity bundleEntity);
        Task<int> DeleteBundle(Guid bundleId);
        Task<BundleEntity> GetBundleById(Guid bundleId);
        Task<List<BundleEntity>> GetBundles();
    }
}
