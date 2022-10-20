using AutoMapper;
using DataAccess.BundleProductRepository;
using DataAccess.BundleRepository;
using DataTransferObjects.BundleDTOs;
using Entities;

namespace Domain.BundleService
{
    public class BundleService : IBundleService
    {
        private readonly IMapper _mapper;
        private readonly IBundleRepository _bundleRepository;
        private readonly IBundleProductRepository _bundleProductRepository;
        public BundleService(IBundleRepository bundleRepository,
                             IBundleProductRepository bundleProductRepository,
                             IMapper mapper)
        {
            _bundleRepository = bundleRepository ?? throw new ArgumentNullException(nameof(bundleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bundleProductRepository = bundleProductRepository ?? throw new ArgumentNullException(nameof(bundleProductRepository));
        }
        public async Task<bool> CreateBundle(CreateBundleDTO bundleDTO)
        {
            BundleEntity bundleEntity = _mapper.Map<BundleEntity>(bundleDTO);
            Guid createdBundleId = await _bundleRepository.CreateBundle(bundleEntity);
            if (createdBundleId == Guid.Empty)
                return false;

            if (!await AddProductsToBundle(createdBundleId, bundleEntity.ProductIds))
            {
                Console.WriteLine("Products addition failed!");
            }

            return true;
        }

        public async Task<bool> UpdateBundle(UpdateBundleDTO bundleDTO)
        {
            BundleEntity bundleEntity = _mapper.Map<BundleEntity>(bundleDTO);
            int rowsAffected = await _bundleRepository.UpdateBundle(bundleEntity);
            if (rowsAffected != 1)
                return false;

            if (!await ReplaceBundleProducts(bundleEntity.Id, bundleEntity.ProductIds))
                return false;

            return true;
        }

        public async Task<bool> DeleteBundle(Guid bundleId)
        {
            int rowsAffected = await _bundleRepository.DeleteBundle(bundleId);
            if (rowsAffected == 0)
                return false;
            return true;
        }

        public async Task<GetBundleDTO> GetBundle(Guid bundleId)
        {
            BundleEntity bundleEntity = await _bundleRepository.GetBundleById(bundleId);
            if (!string.IsNullOrEmpty(bundleEntity.Name))
            {
                GetBundleDTO bundleDTO = _mapper.Map<GetBundleDTO>(bundleEntity);
                return bundleDTO;
            }
            return new GetBundleDTO();
        }

        public async Task<List<GetBundleDTO>> GetBundles()
        {
            List<BundleEntity> bundleEntities = await _bundleRepository.GetBundles();
            List<GetBundleDTO> bundleDTOs = new List<GetBundleDTO>();
            foreach (BundleEntity bundleEntity in bundleEntities)
            {
                bundleDTOs.Add(_mapper.Map<GetBundleDTO>(bundleEntity));
            }

            return bundleDTOs;
        }

        private async Task<bool> ReplaceBundleProducts(Guid bundleId, IEnumerable<Guid> productIds)
        {
            if (!await DeleteProductsFromBundle(bundleId))
                return false;

            if (!await AddProductsToBundle(bundleId, productIds))
                return false;

            return true;
        }

        private async Task<bool> AddProductsToBundle(Guid bundleId, IEnumerable<Guid> productIds)
        {
            List<BundleProductEntity> bundleProductEntities = new List<BundleProductEntity>();
            foreach (Guid productId in productIds)
            {
                bundleProductEntities.Add(new BundleProductEntity(bundleId, productId));
            }

            var rowsAffected = await _bundleProductRepository.AddProductsToBundle(bundleProductEntities);
            if (rowsAffected != 0)
                return true;
            return false;
        }

        private async Task<bool> DeleteProductsFromBundle(Guid bundleId)
        {
            int rowsAffected = await _bundleProductRepository.DeleteProductsFromBundle(bundleId);
            if (rowsAffected >= 0)
                return true;
            return false;
        }
    }
}
