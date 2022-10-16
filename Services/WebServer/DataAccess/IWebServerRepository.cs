using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IWebServerRepository
    {
        #region user actions

        Task<int> CreateUser(UserEntity userEntity);
        Task<int> UpdateUser(UserEntity userEntity);
        Task<int> DeleteUser(Guid userId);
        Task<UserEntity> GetUserById(Guid userId);

        #endregion user actions

        #region product actions

        Task<int> CreateProduct(ProductEntity productEntity);
        Task<int> UpdateProduct(ProductEntity productEntity);
        Task<int> DeleteProduct(Guid productId);
        Task<ProductEntity> GetProductById(Guid productId);
        Task<List<ProductEntity>> GetProducts();
        Task<List<ProductEntity>> GetAvailableProducts();

        #endregion product actions

        #region bundle actions

        Task<Guid> CreateBundle(BundleEntity bundleEntity);
        Task<int> UpdateBundle(BundleEntity bundleEntity);
        Task<int> DeleteBundle(Guid bundleId);
        Task<BundleEntity> GetBundleById(Guid bundleId);
        Task<List<BundleEntity>> GetBundles();

        #endregion bundle actions

        #region bundle product actions

        Task<int> AddProductsToBundle(List<BundleProductEntity> bundleProductEntities);
        Task<int> DeleteProductsFromBundle(Guid id);

        #endregion bundle product actions

        #region department actions

        Task<int> CreateDepartment(DepartmentEntity userEntity);
        Task<int> UpdateDepartment(DepartmentEntity userEntity);
        Task<int> DeleteDepartment(int departmentId);
        Task<DepartmentEntity> GetDepartmentById(int departmentId);
        Task<List<DepartmentEntity>> GetDepartments();

        #endregion department actions

        #region product type actions

        Task<int> CreateProductType(ProductTypeEntity productTypeEntity);
        Task<int> UpdateProductType(ProductTypeEntity productTypeEntity);
        Task<int> DeleteProductType(int idproductTypeId);
        Task<ProductTypeEntity> GetProductTypeById(int productTypeId);
        Task<List<ProductTypeEntity>> GetProductTypes();

        #endregion product type actions

        #region user item actions

        Task<int> CreateUserItem(UserItemEntity userItemEntity);
        Task<int> MarkUserItemAsReceived(Guid userItemId);
        Task<int> DeleteUserItem(Guid userItemId);
        Task<List<UserItemEntity>> GetUserItems(Guid userId);
        Task<List<UserItemEntity>> GetNotReceivedUserItems(Guid userId);

        #endregion user item actions

        #region user perk actions

        Task<int> CreateUserPerk(UserPerkEntity userPerkEntity);
        Task<int> DeleteUserPerk(Guid userPerkId);
        Task<List<UserPerkEntity>> GetUserPerks(Guid userId);

        #endregion user perk actions
    }
}
