using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserPerkService
{
    public interface IUserPerkService
    {
        Task<bool> CreateUserPerk(CreateUserPerkDTO userPerkDTO);
        Task<bool> DeleteUserPerk(Guid userPerkId);
        Task<List<GetUserPerkDTO>> GetUserPerks(Guid userId);
    }
}
