using AutoMapper;
using DataAccess;
using DataTransferObjects;
using Entities;

namespace Domain.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IWebServerRepository _webServerRepo;
        public UserService(IWebServerRepository appServerRepo, IMapper mapper)
        {
            _webServerRepo = appServerRepo ?? throw new ArgumentNullException(nameof(appServerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateUser(CreateUserDTO userDTO)
        {
            UserEntity userEntity = _mapper.Map<UserEntity>(userDTO);
            int rowsAffected = await _webServerRepo.CreateUser(userEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetUserDTO> GetUser(Guid userId)
        {
            UserEntity userEntity = await _webServerRepo.GetUserById(userId);
            if (!string.IsNullOrEmpty(userEntity.Username))
            {
                GetUserDTO userDTO = _mapper.Map<GetUserDTO>(userEntity);
                return userDTO;
            }
            return new GetUserDTO();
        }

        public async Task<bool> UpdateUser(UpdateUserDTO userDTO)
        {
            UserEntity userEntity = _mapper.Map<UserEntity>(userDTO);
            int rowsAffected = await _webServerRepo.UpdateUser(userEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            int rowsAffected = await _webServerRepo.DeleteUser(userId);
            if (rowsAffected == 1)
                return true;
            return false;
        }
    }
}
