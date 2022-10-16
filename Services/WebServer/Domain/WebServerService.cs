using AutoMapper;
using DataAccess;
using DataTransferObjects;
using Entities;

namespace Domain
{
    public class WebServerService : IWebServerService
    {
        private readonly IWebServerRepository _webServerRepo;
        private readonly IMapper _mapper;

        public WebServerService(IWebServerRepository appServerRepo, IMapper mapper)
        {
            _webServerRepo = appServerRepo ?? throw new ArgumentNullException(nameof(appServerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region user actions

        public async Task<bool> CreateUser(CreateUserDTO userDTO)
        {
            UserEntity userEntity = _mapper.Map<UserEntity>(userDTO);
            int rowsAffected = await _webServerRepo.CreateUser(userEntity);
            if(rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetUserDTO> GetUser(Guid userId)
        {
            UserEntity userEntity = await _webServerRepo.GetUserById(userId);
            if(!string.IsNullOrEmpty(userEntity.Username))
            {
                GetUserDTO userDTO = _mapper.Map<GetUserDTO>(userEntity);
                return userDTO;
            }
            return new GetUserDTO();
        }

        public async Task<bool> UpdateUser(UpdateUserDTO userDTO)
        {
            UserEntity userEntity = _mapper.Map<UserEntity>(userDTO);
            int rowsAffected = await _webServerRepo.UpdateUser(userEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            int rowsAffected = await _webServerRepo.DeleteUser(userId);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        #endregion user actions

        #region product actions

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

        #endregion product actions

        #region bundle actions

        public async Task<bool> CreateBundle(CreateBundleDTO bundleDTO)
        {
            BundleEntity bundleEntity = _mapper.Map<BundleEntity>(bundleDTO);
            Guid createdBundleId = await _webServerRepo.CreateBundle(bundleEntity);
            if (createdBundleId == Guid.Empty)
                return false;

            if(!await AddProductsToBundle(createdBundleId, bundleEntity.ProductIds))
            {
                Console.WriteLine("Products addition failed!");
            }

            return true;
        }

        public async Task<bool> UpdateBundle(UpdateBundleDTO bundleDTO)
        {
            BundleEntity bundleEntity = _mapper.Map<BundleEntity>(bundleDTO);
            int rowsAffected = await _webServerRepo.UpdateBundle(bundleEntity);
            if (rowsAffected != 1)
                return false;

            if (!await ReplaceBundleProducts(bundleEntity.Id, bundleEntity.ProductIds))
                return false;

            return true;
        }

        public async Task<bool> DeleteBundle(Guid bundleId)
        {
            int rowsAffected = await _webServerRepo.DeleteBundle(bundleId);
            if (rowsAffected == 0)
                return false;
            return true;
        }

        public async Task<GetBundleDTO> GetBundle(Guid bundleId)
        {
            BundleEntity bundleEntity = await _webServerRepo.GetBundleById(bundleId);
            if (!string.IsNullOrEmpty(bundleEntity.Name))
            {
                GetBundleDTO bundleDTO = _mapper.Map<GetBundleDTO>(bundleEntity);
                return bundleDTO;
            }
            return new GetBundleDTO();
        }

        public async Task<List<GetBundleDTO>> GetBundles()
        {
            List<BundleEntity> bundleEntities = await _webServerRepo.GetBundles();
            List<GetBundleDTO> bundleDTOs = new List<GetBundleDTO>();
            foreach (BundleEntity bundleEntity in bundleEntities)
            {
                bundleDTOs.Add(_mapper.Map<GetBundleDTO>(bundleEntity));
            }

            return bundleDTOs;
        }

        #endregion bundle actions

        #region bundle product actions

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
            foreach(Guid productId in productIds)
            {
                bundleProductEntities.Add(new BundleProductEntity(bundleId, productId));
            }

            var rowsAffected = await _webServerRepo.AddProductsToBundle(bundleProductEntities);
            if (rowsAffected != 0)
                return true;
            return false;
        }

        private async Task<bool> DeleteProductsFromBundle(Guid bundleId)
        {
            int rowsAffected = await _webServerRepo.DeleteProductsFromBundle(bundleId);
            if (rowsAffected >= 0)
                return true;
            return false;
        }

        #endregion bundle product actions

        #region department actions

        public async Task<bool> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            DepartmentEntity departmentEntity = _mapper.Map<DepartmentEntity>(departmentDTO);
            int rowsAffected = await _webServerRepo.CreateDepartment(departmentEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetDepartmentDTO> GetDepartment(int departmentId)
        {
            DepartmentEntity departmentEntity = await _webServerRepo.GetDepartmentById(departmentId);
            if (!string.IsNullOrEmpty(departmentEntity.Name))
            {
                GetDepartmentDTO departmentDTO = _mapper.Map<GetDepartmentDTO>(departmentEntity);
                return departmentDTO;
            }
            return new GetDepartmentDTO();
        }

        public async Task<bool> UpdateDepartment(UpdateDepartmentDTO departmentDTO)
        {
            DepartmentEntity departmentEntity = _mapper.Map<DepartmentEntity>(departmentDTO);
            int rowsAffected = await _webServerRepo.UpdateDepartment(departmentEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            int rowsAffected = await _webServerRepo.DeleteDepartment(departmentId);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<List<GetDepartmentDTO>> GetDepartments()
        {
            List<DepartmentEntity> departmentEntities = await _webServerRepo.GetDepartments();
            List<GetDepartmentDTO> departmentDTOs = new List<GetDepartmentDTO>();
            foreach (DepartmentEntity departmentEntity in departmentEntities)
            {
                departmentDTOs.Add(_mapper.Map<GetDepartmentDTO>(departmentEntity));
            }
            return departmentDTOs;
        }

        #endregion department actions

        #region product type actions

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

        #endregion product type actions

        #region user item actions

        public async Task<bool> CreateUserItem(CreateUserItemDTO userItemDTO)
        {
            UserItemEntity userItemEntity = _mapper.Map<UserItemEntity>(userItemDTO);
            int rowsAffected = await _webServerRepo.CreateUserItem(userItemEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> MarkUserItemAsReceived(Guid userItemId)
        {
            int rowsAffected = await _webServerRepo.MarkUserItemAsReceived(userItemId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> DeleteUserItem(Guid userItemId)
        {
            int rowsAffected = await _webServerRepo.DeleteUserItem(userItemId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<List<GetUserItemDTO>> GetUserItems(Guid userId)
        {
            List<UserItemEntity> userItemEntities = await _webServerRepo.GetUserItems(userId);
            List<GetUserItemDTO> userItemDTOs = new List<GetUserItemDTO>();
            foreach (UserItemEntity userItemEntity in userItemEntities)
            {
                userItemDTOs.Add(_mapper.Map<GetUserItemDTO>(userItemEntity));
            }
            return userItemDTOs;
        }
        public async Task<List<GetNotReceivedItemDTO>> GetNotReceivedUserItems(Guid userId)
        {
            List<UserItemEntity> userItemEntities = await _webServerRepo.GetNotReceivedUserItems(userId);
            List<GetNotReceivedItemDTO> userItemDTOs = new List<GetNotReceivedItemDTO>();
            foreach (UserItemEntity userItemEntity in userItemEntities)
            {
                userItemDTOs.Add(_mapper.Map<GetNotReceivedItemDTO>(userItemEntity));
            }
            return userItemDTOs;
        }

        #endregion user item actions
    }
}
