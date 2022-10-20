using DataTransferObjects.ProductTypeDTOs;

namespace Domain.ProductTypeService
{
    public interface IProductTypeService
    {
        Task<bool> CreateProductType(CreateProductTypeDTO userDTO);
        Task<bool> UpdateProductType(UpdateProductTypeDTO userDTO);
        Task<bool> DeleteProductType(int productTypeId);
        Task<GetProductTypeDTO> GetProductType(int productTypeId);
        Task<List<GetProductTypeDTO>> GetProductTypes();
    }
}
