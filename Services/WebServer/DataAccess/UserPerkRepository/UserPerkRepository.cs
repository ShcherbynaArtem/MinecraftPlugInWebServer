using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.UserPerkRepository
{
    public class UserPerkRepository : IUserPerkRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserPerkRepository> _logger;

        public UserPerkRepository(IConfiguration configuration, ILogger<UserPerkRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MapSqlColumns();
        }

        private void MapSqlColumns()
        {
            SqlMapper.SetTypeMap(typeof(UserPerkEntity), new CustomPropertyTypeMap(
                typeof(UserPerkEntity), (type, columnName) => type.GetProperties().FirstOrDefault(prop =>
                prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
        }

        public async Task<int> CreateUserPerk(UserPerkEntity userPerkEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO user_perks (user_id, perk_id) VALUES (@UserId, @PerkId)",
                    new { UserId = userPerkEntity.UserId, PerkId = userPerkEntity.PerkId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while CreateUserPerk query");
                return -1;
            }
        }
        public async Task<int> DeleteUserPerk(Guid userPerkId)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM user_perks WHERE id = @Id", new { Id = userPerkId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while DeleteUserPerk query");
                return -1;
            }
        }
        public async Task<List<UserPerkEntity>> GetUserPerks(Guid userId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var userPerks = await connection.QueryAsync<UserPerkEntity>
                    ("SELECT id, user_id, perk_id FROM user_perks WHERE user_id = @UserId",
                    new { UserId = userId });

                return (List<UserPerkEntity>)userPerks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetUserPerks query");
                return new List<UserPerkEntity>();
            }
        }
    }
}
