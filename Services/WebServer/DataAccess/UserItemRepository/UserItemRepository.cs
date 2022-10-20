using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.UserItemRepository
{
    public class UserItemRepository : IUserItemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserItemRepository> _logger;


        public UserItemRepository(IConfiguration configuration, ILogger<UserItemRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MapSqlColumns();
        }

        private void MapSqlColumns()
        {
            SqlMapper.SetTypeMap(typeof(UserItemEntity), new CustomPropertyTypeMap(
                typeof(UserItemEntity), (type, columnName) => type.GetProperties().FirstOrDefault(prop =>
                prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
        }

        public async Task<int> CreateUserItem(UserItemEntity userItemEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO user_items (user_id, item_id) VALUES (@UserId, @ItemId)",
                    new { UserId = userItemEntity.UserId, ItemId = userItemEntity.ItemId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while CreateUserItem query");
                return -1;
            }
        }
        public async Task<int> MarkUserItemAsReceived(Guid userItemId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE user_items SET received = true WHERE id = @UserItemId",
                   new { UserItemId = userItemId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while MarkUserItemAsReceived query");
                return -1;
            }
        }
        public async Task<int> DeleteUserItem(Guid userItemId)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM user_items WHERE id = @Id", new { Id = userItemId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while DeleteUserItem query");
                return -1;
            }
        }
        public async Task<List<UserItemEntity>> GetUserItems(Guid userId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var userItems = await connection.QueryAsync<UserItemEntity>
                    ("SELECT id, user_id, item_id, received FROM user_items WHERE user_id = @UserId",
                    new { UserId = userId });

                return (List<UserItemEntity>)userItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetUserItems query");
                return new List<UserItemEntity>();
            }
        }
        public async Task<List<UserItemEntity>> GetNotReceivedUserItems(Guid userId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var userItems = await connection.QueryAsync<UserItemEntity>
                    (@"SELECT id, user_id, item_id, received FROM user_items
                       WHERE user_id = @UserId AND received = false",
                    new { UserId = userId });

                return (List<UserItemEntity>)userItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetNotReceivedUserItems query");
                return new List<UserItemEntity>();
            }
        }
    }
}
