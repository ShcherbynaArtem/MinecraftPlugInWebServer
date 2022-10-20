using Entities;

namespace DataAccess.UserRepository
{
    public interface IUserRepository
    {
        Task<int> CreateUser(UserEntity userEntity);
        Task<int> UpdateUser(UserEntity userEntity);
        Task<int> DeleteUser(Guid userId);
        Task<UserEntity> GetUserById(Guid userId);
    }
}
