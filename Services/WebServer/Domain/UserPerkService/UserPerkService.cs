using AutoMapper;
using DataAccess;
using DataTransferObjects;
using Entities;

namespace Domain.UserPerkService
{
    public class UserPerkService : IUserPerkService
    {
        private readonly IMapper _mapper;
        private readonly IWebServerRepository _webServerRepo;
        public UserPerkService(IWebServerRepository appServerRepo, IMapper mapper)
        {
            _webServerRepo = appServerRepo ?? throw new ArgumentNullException(nameof(appServerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateUserPerk(CreateUserPerkDTO userPerkDTO)
        {
            UserPerkEntity userPerkEntity = _mapper.Map<UserPerkEntity>(userPerkDTO);
            int rowsAffected = await _webServerRepo.CreateUserPerk(userPerkEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> DeleteUserPerk(Guid userPerkId)
        {
            int rowsAffected = await _webServerRepo.DeleteUserPerk(userPerkId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<List<GetUserPerkDTO>> GetUserPerks(Guid userId)
        {
            List<UserPerkEntity> userPerksEntities = await _webServerRepo.GetUserPerks(userId);
            List<GetUserPerkDTO> userPerkDTOs = new List<GetUserPerkDTO>();
            foreach (UserPerkEntity userPerkEntity in userPerksEntities)
            {
                userPerkDTOs.Add(_mapper.Map<GetUserPerkDTO>(userPerkEntity));
            }
            return userPerkDTOs;
        }
    }
}
