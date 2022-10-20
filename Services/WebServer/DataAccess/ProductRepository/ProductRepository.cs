using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IConfiguration configuration, ILogger<ProductRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateProduct(ProductEntity productEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO products (name, description, type, price) VALUES (@Name, @Description, @Type, @Price)",
                    new { Name = productEntity.Name, Description = productEntity.Description, Type = productEntity.Type, Price = productEntity.Price });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while CreateProduct query");
                return -1;
            }
        }

        public async Task<int> UpdateProduct(ProductEntity productEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    (@"UPDATE products SET name = @Name, description = @Description,
                                           type = @Type, price = @Price, availability = @Availability
                                           WHERE id = @Id",
                   new
                   {
                       Id = productEntity.Id,
                       Name = productEntity.Name,
                       Description = productEntity.Description,
                       Type = productEntity.Type,
                       Price = productEntity.Price,
                       Availability = productEntity.Availability
                   });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while UpdateProduct query");
                return -1;
            }
        }

        public async Task<int> DeleteProduct(Guid productId)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM products WHERE id = @Id", new { Id = productId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while DeleteProduct query");
                return -1;
            }
        }

        public async Task<ProductEntity> GetProductById(Guid productId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var product = await connection.QuerySingleAsync<ProductEntity>
                    (@"SELECT id, name, description, type, price, availability FROM 
                   products WHERE id = @Id",
                    new { Id = productId });

                return (ProductEntity)product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetProductById query");
                return new ProductEntity();
            }
        }

        public async Task<List<ProductEntity>> GetProducts()
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var products = await connection.QueryAsync<ProductEntity>
                    ("SELECT id, name, description, type, price, availability FROM products");

                return (List<ProductEntity>)products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetProducts query");
                return new List<ProductEntity>();
            }
        }

        public async Task<List<ProductEntity>> GetAvailableProducts()
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var products = await connection.QueryAsync<ProductEntity>
                    ("SELECT id, name, description, type, price FROM products WHERE availability = true");

                return (List<ProductEntity>)products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetAvailableProducts query");
                return new List<ProductEntity>();
            }
        }
    }
}
