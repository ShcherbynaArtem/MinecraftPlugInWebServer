using Entities;

namespace DataAccess.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<int> CreateDepartment(DepartmentEntity userEntity);
        Task<int> UpdateDepartment(DepartmentEntity userEntity);
        Task<int> DeleteDepartment(int departmentId);
        Task<DepartmentEntity> GetDepartmentById(int departmentId);
        Task<List<DepartmentEntity>> GetDepartments();
    }
}
