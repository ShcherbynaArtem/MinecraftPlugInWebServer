using DataTransferObjects.UserPerkDTOs;
using Domain.UserPerkService;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserPerkController : ControllerBase
    {
        private readonly IUserPerkService _userPerkService;

        public UserPerkController(IUserPerkService userPerkService)
        {
            _userPerkService = userPerkService ?? throw new ArgumentNullException(nameof(userPerkService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserPerk(CreateUserPerkDTO userPerkDTO)
        {
            if (await _userPerkService.CreateUserPerk(userPerkDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserPerk(Guid userPerkId)
        {
            if (await _userPerkService.DeleteUserPerk(userPerkId))
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<GetUserPerkDTO>> GetUserPerks(Guid userId)
        {
            List<GetUserPerkDTO> userPerkDTOs = await _userPerkService.GetUserPerks(userId);
            return Ok(userPerkDTOs);
        }
    }
}
