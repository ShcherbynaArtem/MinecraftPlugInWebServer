using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.BundleRepository
{
    public class BundleRepository : IBundleRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BundleRepository> _logger;

        public BundleRepository(IConfiguration configuration, ILogger<BundleRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> CreateBundle(BundleEntity bundleEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                Guid createdBundleId = await connection.QuerySingleAsync<Guid>
                    (@"INSERT INTO bundles (name, discount) VALUES (@Name, @Discount) RETURNING id",
                    new { Name = bundleEntity.Name, Discount = bundleEntity.Discount });

                return createdBundleId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while CreateBundle query");
                return Guid.Empty;
            }
        }

        public async Task<int> UpdateBundle(BundleEntity bundleEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE bundles SET name = @Name, discount = @Discount WHERE id = @Id",
                   new { Id = bundleEntity.Id, Name = bundleEntity.Name, Discount = bundleEntity.Discount });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while UpdateBundle query");
                return -1;
            }
        }

        public async Task<int> DeleteBundle(Guid bundleId)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM bundles WHERE id = @Id", new { Id = bundleId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while DeleteBundle query");
                return -1;
            }
        }

        public async Task<BundleEntity> GetBundleById(Guid bundleId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var bundle = await connection.QuerySingleAsync<BundleEntity>
                    (@"SELECT b.id, b.name, b.discount, array_remove(array_agg(bp.product_id), NULL) as ProductIds
                       FROM bundles as b
                       LEFT JOIN bundle_products as bp ON b.id = bp.bundle_id
                       WHERE b.id = @Id
                       GROUP BY b.id",
                       new { Id = bundleId });

                return (BundleEntity)bundle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetBundleById query");
                return new BundleEntity();
            }

        }

        public async Task<List<BundleEntity>> GetBundles()
        {
            try
            {
                _logger.LogInformation("Test message GetBundles()");
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var bundles = await connection.QueryAsync<BundleEntity>
                    (@"SELECT b.id, b.name, b.discount, array_remove(array_agg(bp.product_id), NULL) as ProductIds
                       FROM bundles as b
                       LEFT JOIN bundle_products as bp ON b.id = bp.bundle_id
                       GROUP BY b.id");

                return (List<BundleEntity>)bundles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetBundles query");
                return new List<BundleEntity>();
            }
        }
    }
}
