using AutoMapper;
using DataAccess.UserPerkRepository;
using DataTransferObjects.UserPerkDTOs;
using Entities;

namespace Domain.UserPerkService
{
    public class UserPerkService : IUserPerkService
    {
        private readonly IMapper _mapper;
        private readonly IUserPerkRepository _userPerkRepository;
        public UserPerkService(IUserPerkRepository userPerkRepository, IMapper mapper)
        {
            _userPerkRepository = userPerkRepository ?? throw new ArgumentNullException(nameof(userPerkRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateUserPerk(CreateUserPerkDTO userPerkDTO)
        {
            UserPerkEntity userPerkEntity = _mapper.Map<UserPerkEntity>(userPerkDTO);
            int rowsAffected = await _userPerkRepository.CreateUserPerk(userPerkEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> DeleteUserPerk(Guid userPerkId)
        {
            int rowsAffected = await _userPerkRepository.DeleteUserPerk(userPerkId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<List<GetUserPerkDTO>> GetUserPerks(Guid userId)
        {
            List<UserPerkEntity> userPerksEntities = await _userPerkRepository.GetUserPerks(userId);
            List<GetUserPerkDTO> userPerkDTOs = new List<GetUserPerkDTO>();
            foreach (UserPerkEntity userPerkEntity in userPerksEntities)
            {
                userPerkDTOs.Add(_mapper.Map<GetUserPerkDTO>(userPerkEntity));
            }
            return userPerkDTOs;
        }
    }
}
