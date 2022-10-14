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
        Task<int> DeleteUser(Guid id);
        Task<UserEntity> GetUserById(Guid id);

        #endregion user actions

        #region product actions

        Task<int> CreateProduct(ProductEntity productEntity);
        Task<int> UpdateProduct(ProductEntity productEntity);
        Task<int> DeleteProduct(Guid id);
        Task<ProductEntity> GetProductById(Guid id);
        Task<List<ProductEntity>> GetProducts();
        Task<List<ProductEntity>> GetAvailableProducts();

        #endregion product actions

        #region bundle actions

        Task<Guid> CreateBundle(BundleEntity bundleEntity);
        Task<int> UpdateBundle(BundleEntity bundleEntity);
        Task<int> DeleteBundle(Guid id);
        Task<BundleEntity> GetBundleById(Guid id);
        Task<List<BundleEntity>> GetBundles();

        #endregion bundle actions

        #region bundle product actions

        Task<int> AddProductsToBundle(List<BundleProductEntity> bundleProductEntities);
        Task<int> DeleteProductsFromBundle(Guid id);

        #endregion bundle product actions

        #region department actions

        Task<int> CreateDepartment(DepartmentEntity userEntity);
        Task<int> UpdateDepartment(DepartmentEntity userEntity);
        Task<int> DeleteDepartment(int id);
        Task<DepartmentEntity> GetDepartmentById(int id);
        Task<List<DepartmentEntity>> GetDepartments();

        #endregion department actions

        #region product type actions

        Task<int> CreateProductType(ProductTypeEntity productTypeEntity);
        Task<int> UpdateProductType(ProductTypeEntity productTypeEntity);
        Task<int> DeleteProductType(int id);
        Task<ProductTypeEntity> GetProductTypeById(int id);
        Task<List<ProductTypeEntity>> GetProductTypes();

        #endregion product type actions
    }
}
