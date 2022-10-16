using DataTransferObjects;

namespace Domain
{
    public interface IWebServerService
    {
        #region user actions

        Task<bool> CreateUser(CreateUserDTO userDTO);
        Task<bool> UpdateUser(UpdateUserDTO userDTO);
        Task<bool> DeleteUser(Guid userId);
        Task<GetUserDTO> GetUser(Guid userId);

        #endregion user actions

        #region product actions

        Task<bool> CreateProduct(CreateProductDTO userDTO);
        Task<bool> UpdateProduct(UpdateProductDTO userDTO);
        Task<bool> DeleteProduct(Guid productId);
        Task<GetProductDTO> GetProduct(Guid productId);
        Task<List<GetProductDTO>> GetProducts();
        Task<List<GetAvailableProductDTO>> GetAvailableProducts();

        #endregion product actions

        #region bundle actions

        Task<bool> CreateBundle(CreateBundleDTO bundleDTO);
        Task<bool> UpdateBundle(UpdateBundleDTO bundleDTO);
        Task<bool> DeleteBundle(Guid bundleId);
        Task<GetBundleDTO> GetBundle(Guid bundleId);
        Task<List<GetBundleDTO>> GetBundles();

        #endregion bundle actions

        #region department actions

        Task<bool> CreateDepartment(CreateDepartmentDTO departmentDTO);
        Task<bool> UpdateDepartment(UpdateDepartmentDTO departmentDTO);
        Task<bool> DeleteDepartment(int departmentId);
        Task<GetDepartmentDTO> GetDepartment(int departmentId);
        Task<List<GetDepartmentDTO>> GetDepartments();

        #endregion department actions

        #region product type actions

        Task<bool> CreateProductType(CreateProductTypeDTO userDTO);
        Task<bool> UpdateProductType(UpdateProductTypeDTO userDTO);
        Task<bool> DeleteProductType(int productTypeId);
        Task<GetProductTypeDTO> GetProductType(int productTypeId);
        Task<List<GetProductTypeDTO>> GetProductTypes();

        #endregion product type actions

        #region user item actions

        Task<bool> CreateUserItem(CreateUserItemDTO userItemDTO);
        Task<bool> MarkUserItemAsReceived(Guid userItemId);
        Task<bool> DeleteUserItem(Guid userItemId);
        Task<List<GetUserItemDTO>> GetUserItems(Guid userId);
        Task<List<GetNotReceivedItemDTO>> GetNotReceivedUserItems(Guid userId);

        #endregion user item actions
    }
}