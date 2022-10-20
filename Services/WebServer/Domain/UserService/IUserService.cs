using DataTransferObjects.UserDTOs;

namespace Domain.UserService
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateUserDTO userDTO);
        Task<bool> UpdateUser(UpdateUserDTO userDTO);
        Task<bool> DeleteUser(Guid userId);
        Task<GetUserDTO> GetUser(Guid userId);
    }
}
