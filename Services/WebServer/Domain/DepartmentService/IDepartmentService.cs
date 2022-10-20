using DataTransferObjects.DepartmentDTOs;

namespace Domain.DepartmentService
{
    public interface IDepartmentService
    {
        Task<bool> CreateDepartment(CreateDepartmentDTO departmentDTO);
        Task<bool> UpdateDepartment(UpdateDepartmentDTO departmentDTO);
        Task<bool> DeleteDepartment(int departmentId);
        Task<GetDepartmentDTO> GetDepartment(int departmentId);
        Task<List<GetDepartmentDTO>> GetDepartments();
    }
}
