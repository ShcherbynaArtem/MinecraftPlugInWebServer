using DataTransferObjects;

namespace Domain
{
    public interface IWebServerService
    {
        #region user actions

        Task<bool> CreateUser(CreateUserDTO userDTO);
        Task<bool> UpdateUser(UpdateUserDTO userDTO);
        Task<bool> DeleteUser(Guid id);
        Task<GetUserDTO> GetUser(Guid id);

        #endregion user actions

        #region product actions

        Task<bool> CreateProduct(CreateProductDTO userDTO);
        Task<bool> UpdateProduct(UpdateProductDTO userDTO);
        Task<bool> DeleteProduct(Guid id);
        Task<GetProductDTO> GetProduct(Guid id);
        Task<List<GetProductDTO>> GetProducts();
        Task<List<GetAvailableProductDTO>> GetAvailableProducts();

        #endregion product actions

        #region bundle actions

        Task<bool> CreateBundle(CreateBundleDTO bundleDTO);
        Task<bool> UpdateBundle(UpdateBundleDTO bundleDTO);
        Task<bool> DeleteBundle(Guid id);
        Task<GetBundleDTO> GetBundle(Guid id);
        Task<List<GetBundleDTO>> GetBundles();

        #endregion bundle actions

        #region department actions

        Task<bool> CreateDepartment(CreateDepartmentDTO departmentDTO);
        Task<bool> UpdateDepartment(UpdateDepartmentDTO departmentDTO);
        Task<bool> DeleteDepartment(int id);
        Task<GetDepartmentDTO> GetDepartment(int id);
        Task<List<GetDepartmentDTO>> GetDepartments();

        #endregion department actions

        #region product type actions

        Task<bool> CreateProductType(CreateProductTypeDTO userDTO);
        Task<bool> UpdateProductType(UpdateProductTypeDTO userDTO);
        Task<bool> DeleteProductType(int id);
        Task<GetProductTypeDTO> GetProductType(int id);
        Task<List<GetProductTypeDTO>> GetProductTypes();

        #endregion product type actions

        #region user item actions

        Task<bool> CreateUserItem(CreateUserItemDTO userItemDTO);
        Task<bool> MarkUserItemAsReceived(Guid userItemId);
        Task<bool> DeleteUserItem(Guid id);
        Task<List<GetUserItemDTO>> GetUserItems(Guid userId);
        Task<List<GetNotReceivedItemDTO>> GetNotReceivedUserItems(Guid userId);

        #endregion user item actions
    }
}