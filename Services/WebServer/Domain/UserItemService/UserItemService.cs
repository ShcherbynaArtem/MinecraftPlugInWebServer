using AutoMapper;
using DataAccess.UserItemRepository;
using DataTransferObjects.UserItemDTOs;
using Entities;

namespace Domain.UserItemService
{
    public class UserItemService : IUserItemService
    {
        private readonly IMapper _mapper;
        private readonly IUserItemRepository _userItemRepository;
        public UserItemService(IUserItemRepository userItemRepository, IMapper mapper)
        {
            _userItemRepository = userItemRepository ?? throw new ArgumentNullException(nameof(userItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateUserItem(CreateUserItemDTO userItemDTO)
        {
            UserItemEntity userItemEntity = _mapper.Map<UserItemEntity>(userItemDTO);
            int rowsAffected = await _userItemRepository.CreateUserItem(userItemEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> MarkUserItemAsReceived(Guid userItemId)
        {
            int rowsAffected = await _userItemRepository.MarkUserItemAsReceived(userItemId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> DeleteUserItem(Guid userItemId)
        {
            int rowsAffected = await _userItemRepository.DeleteUserItem(userItemId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<List<GetUserItemDTO>> GetUserItems(Guid userId)
        {
            List<UserItemEntity> userItemEntities = await _userItemRepository.GetUserItems(userId);
            List<GetUserItemDTO> userItemDTOs = new List<GetUserItemDTO>();
            foreach (UserItemEntity userItemEntity in userItemEntities)
            {
                userItemDTOs.Add(_mapper.Map<GetUserItemDTO>(userItemEntity));
            }
            return userItemDTOs;
        }
        public async Task<List<GetNotReceivedItemDTO>> GetNotReceivedUserItems(Guid userId)
        {
            List<UserItemEntity> userItemEntities = await _userItemRepository.GetNotReceivedUserItems(userId);
            List<GetNotReceivedItemDTO> userItemDTOs = new List<GetNotReceivedItemDTO>();
            foreach (UserItemEntity userItemEntity in userItemEntities)
            {
                userItemDTOs.Add(_mapper.Map<GetNotReceivedItemDTO>(userItemEntity));
            }
            return userItemDTOs;
        }
    }
}
