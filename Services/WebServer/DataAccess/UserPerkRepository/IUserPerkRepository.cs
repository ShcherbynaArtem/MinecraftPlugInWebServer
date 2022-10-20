using Entities;

namespace DataAccess.UserPerkRepository
{
    public interface IUserPerkRepository
    {
        Task<int> CreateUserPerk(UserPerkEntity userPerkEntity);
        Task<int> DeleteUserPerk(Guid userPerkId);
        Task<List<UserPerkEntity>> GetUserPerks(Guid userId);
    }
}
