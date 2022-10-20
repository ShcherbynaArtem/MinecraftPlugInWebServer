using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;
using Z.Dapper.Plus;

namespace DataAccess.BundleProductRepository
{
    public class BundleProductRepository : IBundleProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BundleProductRepository> _logger;

        public BundleProductRepository(IConfiguration configuration, ILogger<BundleProductRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MapSqlColumns();
        }

        private void MapSqlColumns()
        {
            SqlMapper.SetTypeMap(typeof(BundleProductEntity), new CustomPropertyTypeMap(
                typeof(BundleProductEntity), (type, columnName) => type.GetProperties().FirstOrDefault(prop =>
                prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
        }

        public async Task<int> AddProductsToBundle(List<BundleProductEntity> bundleProducts)
        {
            try
            {
                DapperPlusManager.Entity<BundleProductEntity>().Table("bundle_products");
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));

                await connection.BulkActionAsync(x => x.BulkInsert<BundleProductEntity>(bundleProducts));

                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while AddProductsToBundle query");
                return -1;
            }
        }

        public async Task<int> DeleteProductsFromBundle(Guid bundleId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    (@"DELETE FROM bundle_products WHERE bundle_id = @Id",
                    new { Id = bundleId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while DeleteProductsFromBundle query");
                return -1;
            }
        }
    }
}
