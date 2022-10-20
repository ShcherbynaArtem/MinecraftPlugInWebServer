using DataTransferObjects.UserPerkDTOs;

namespace Domain.UserPerkService
{
    public interface IUserPerkService
    {
        Task<bool> CreateUserPerk(CreateUserPerkDTO userPerkDTO);
        Task<bool> DeleteUserPerk(Guid userPerkId);
        Task<List<GetUserPerkDTO>> GetUserPerks(Guid userId);
    }
}
