using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateUser(UserEntity userEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO users (id, username, email, ip) VALUES (@Id, @Username, @Email, @Ip)",
                    new { Id = userEntity.Id, Username = userEntity.Username, Email = userEntity.Email, Ip = userEntity.Ip });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while CreateUser query");
                return -1;
            }
        }

        public async Task<int> UpdateUser(UserEntity userEntity)
        {

            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE users SET username = @Username, email = @Email, ip = @Ip, lives = @Lives WHERE id = @Id",
                    new { Id = userEntity.Id, Username = userEntity.Username, Email = userEntity.Email, Ip = userEntity.Ip, Lives = userEntity.Lives });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while UpdateUser query");
                return -1;
            }
        }

        public async Task<int> DeleteUser(Guid userId)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM users WHERE id = @Id", new { Id = userId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while DeleteUser query");
                return -1;
            }
        }

        public async Task<UserEntity> GetUserById(Guid userId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var user = await connection.QuerySingleAsync<UserEntity>
                    (@"SELECT id, username, email, ip, lives FROM 
                   users WHERE id = @Id",
                    new { Id = userId });

                return (UserEntity)user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while GetUserById query");
                return new UserEntity();
            }
        }
    }
}
