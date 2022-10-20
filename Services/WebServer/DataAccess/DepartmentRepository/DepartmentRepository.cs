using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(IConfiguration configuration, ILogger<DepartmentRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateDepartment(DepartmentEntity departmentEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO departments (id, name, description) VALUES (@Id, @Name, @Description)",
                    new { Id = departmentEntity.Id, Name = departmentEntity.Name, Description = departmentEntity.Description });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while CreateDepartment query");
                return -1;
            }
        }

        public async Task<int> UpdateDepartment(DepartmentEntity departmentEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE departments SET name = @Name, description = @Description WHERE id = @Id",
                    new { Id = departmentEntity.Id, Name = departmentEntity.Name, Description = departmentEntity.Description });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while UpdateDepartment query");
                return -1;
            }
        }

        public async Task<int> DeleteDepartment(int departmentId)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM departments WHERE id = @Id", new { Id = departmentId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while DeleteDepartment query");
                return -1;
            }
        }

        public async Task<DepartmentEntity> GetDepartmentById(int departmentId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var department = await connection.QuerySingleAsync<DepartmentEntity>
                    (@"SELECT id, name, description FROM departments WHERE id = @Id",
                    new { Id = departmentId });

                return (DepartmentEntity)department;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetDepartmentById query");
                return new DepartmentEntity();
            }
        }

        public async Task<List<DepartmentEntity>> GetDepartments()
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var departments = await connection.QueryAsync<DepartmentEntity>
                    ("SELECT id, name, description FROM departments");

                return (List<DepartmentEntity>)departments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetDepartments query");
                return new List<DepartmentEntity>();
            }
        }
    }
}
