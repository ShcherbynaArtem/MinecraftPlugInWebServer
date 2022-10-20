using AutoMapper;
using DataAccess;
using DataTransferObjects;
using Entities;

namespace Domain.UserItemService
{
    public class UserItemService : IUserItemService
    {
        private readonly IMapper _mapper;
        private readonly IWebServerRepository _webServerRepo;
        public UserItemService(IWebServerRepository appServerRepo, IMapper mapper)
        {
            _webServerRepo = appServerRepo ?? throw new ArgumentNullException(nameof(appServerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateUserItem(CreateUserItemDTO userItemDTO)
        {
            UserItemEntity userItemEntity = _mapper.Map<UserItemEntity>(userItemDTO);
            int rowsAffected = await _webServerRepo.CreateUserItem(userItemEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> MarkUserItemAsReceived(Guid userItemId)
        {
            int rowsAffected = await _webServerRepo.MarkUserItemAsReceived(userItemId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> DeleteUserItem(Guid userItemId)
        {
            int rowsAffected = await _webServerRepo.DeleteUserItem(userItemId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<List<GetUserItemDTO>> GetUserItems(Guid userId)
        {
            List<UserItemEntity> userItemEntities = await _webServerRepo.GetUserItems(userId);
            List<GetUserItemDTO> userItemDTOs = new List<GetUserItemDTO>();
            foreach (UserItemEntity userItemEntity in userItemEntities)
            {
                userItemDTOs.Add(_mapper.Map<GetUserItemDTO>(userItemEntity));
            }
            return userItemDTOs;
        }
        public async Task<List<GetNotReceivedItemDTO>> GetNotReceivedUserItems(Guid userId)
        {
            List<UserItemEntity> userItemEntities = await _webServerRepo.GetNotReceivedUserItems(userId);
            List<GetNotReceivedItemDTO> userItemDTOs = new List<GetNotReceivedItemDTO>();
            foreach (UserItemEntity userItemEntity in userItemEntities)
            {
                userItemDTOs.Add(_mapper.Map<GetNotReceivedItemDTO>(userItemEntity));
            }
            return userItemDTOs;
        }
    }
}
