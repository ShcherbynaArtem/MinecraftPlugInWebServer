using DataTransferObjects.UserItemDTOs;

namespace Domain.UserItemService
{
    public interface IUserItemService
    {
        Task<bool> CreateUserItem(CreateUserItemDTO userItemDTO);
        Task<bool> MarkUserItemAsReceived(Guid userItemId);
        Task<bool> DeleteUserItem(Guid userItemId);
        Task<List<GetUserItemDTO>> GetUserItems(Guid userId);
        Task<List<GetNotReceivedItemDTO>> GetNotReceivedUserItems(Guid userId);
    }
}
