using Entities;

namespace DataAccess.UserItemRepository
{
    public interface IUserItemRepository
    {
        Task<int> CreateUserItem(UserItemEntity userItemEntity);
        Task<int> MarkUserItemAsReceived(Guid userItemId);
        Task<int> DeleteUserItem(Guid userItemId);
        Task<List<UserItemEntity>> GetUserItems(Guid userId);
        Task<List<UserItemEntity>> GetNotReceivedUserItems(Guid userId);
    }
}
