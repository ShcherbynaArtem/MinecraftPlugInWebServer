using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.ProductTypeRepository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductTypeRepository> _logger;

        public ProductTypeRepository(IConfiguration configuration, ILogger<ProductTypeRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateProductType(ProductTypeEntity productTypeEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO product_types (id, name, department) VALUES (@Id, @Name, @Department)",
                    new { Id = productTypeEntity.Id, Name = productTypeEntity.Name, Department = productTypeEntity.Department });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while CreateProductType query");
                return -1;
            }
        }

        public async Task<int> UpdateProductType(ProductTypeEntity productTypeEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE product_types SET name = @Name, department = @Department WHERE id = @Id",
                   new { Id = productTypeEntity.Id, Name = productTypeEntity.Name, Department = productTypeEntity.Department });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while UpdateProductType query");
                return -1;
            }
        }

        public async Task<int> DeleteProductType(int productTypeId)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM product_types WHERE id = @Id", new { Id = productTypeId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while DeleteProductType query");
                return -1;
            }
        }

        public async Task<ProductTypeEntity> GetProductTypeById(int productTypeId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var productType = await connection.QuerySingleAsync<ProductTypeEntity>
                    (@"SELECT id, name, department FROM product_types WHERE id = @Id",
                    new { Id = productTypeId });

                return (ProductTypeEntity)productType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetProductTypeById query");
                return new ProductTypeEntity();
            }
        }

        public async Task<List<ProductTypeEntity>> GetProductTypes()
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var productTypes = await connection.QueryAsync<ProductTypeEntity>
                    ("SELECT id, name, department FROM product_types");

                return (List<ProductTypeEntity>)productTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetProductTypes query");
                return new List<ProductTypeEntity>();
            }
        }
    }
}
