using DataTransferObjects;

namespace Domain
{
    public interface IWebServerService
    {
        #region user actions

        Task<bool> CreateUser(UserDTO userDTO);
        Task<UserDTO> GetUser(Guid id);
        Task<bool> UpdateUser(UserDTO userDTO);
        Task<bool> DeleteUser(Guid id);

        #endregion user actions

        #region product actions

        Task<bool> CreateProduct(ProductDTO userDTO);
        Task<bool> UpdateProduct(ProductDTO userDTO);
        Task<bool> DeleteProduct(Guid id);
        Task<ProductDTO> GetProduct(Guid id);
        Task<List<ProductDTO>> GetProducts();

        #endregion product actions

        #region bundle actions

        Task<bool> CreateBundle(BundleDTO bundleDTO);
        Task<BundleDTO> GetBundle(Guid id);
        Task<bool> UpdateBundle(BundleDTO bundleDTO);
        Task<bool> DeleteBundle(Guid id);
        Task<List<BundleDTO>> GetBundles();

        #endregion bundle actions

        #region bundle product actions

        Task<bool> AddProductToBundle(BundleProductDTO bundleProductDTO);
        Task<bool> DeleteProductFromBundle(BundleProductDTO bundleProductDTO);

        #endregion bundle product actions

        #region department actions

        Task<bool> CreateDepartment(CreateDepartmentDTO departmentDTO);
        Task<GetDepartmentDTO> GetDepartment(int id);
        Task<bool> UpdateDepartment(UpdateDepartmentDTO departmentDTO);
        Task<bool> DeleteDepartment(int id);
        Task<List<GetDepartmentDTO>> GetDepartments();

        #endregion department actions

        #region product type actions

        Task<bool> CreateProductType(CreateProductTypeDTO userDTO);
        Task<bool> UpdateProductType(UpdateProductTypeDTO userDTO);
        Task<bool> DeleteProductType(int id);
        Task<GetProductTypeDTO> GetProductType(int id);
        Task<List<GetProductTypeDTO>> GetProductTypes();

        #endregion product type actions
    }
}