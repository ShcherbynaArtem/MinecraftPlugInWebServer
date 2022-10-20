using DataTransferObjects.BundleDTOs;

namespace Domain.BundleService
{
    public interface IBundleService
    {
        Task<bool> CreateBundle(CreateBundleDTO bundleDTO);
        Task<bool> UpdateBundle(UpdateBundleDTO bundleDTO);
        Task<bool> DeleteBundle(Guid bundleId);
        Task<GetBundleDTO> GetBundle(Guid bundleId);
        Task<List<GetBundleDTO>> GetBundles();
    }
}
