using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserPerkController : ControllerBase
    {
        private readonly IWebServerService _webServerService;

        public UserPerkController(IWebServerService webServerService)
        {
            _webServerService = webServerService ?? throw new ArgumentNullException(nameof(webServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserPerk(CreateUserPerkDTO userPerkDTO)
        {
            if (await _webServerService.CreateUserPerk(userPerkDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserPerk(Guid userPerkId)
        {
            if (await _webServerService.DeleteUserPerk(userPerkId))
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<GetUserPerkDTO>> GetUserPerks(Guid userId)
        {
            List<GetUserPerkDTO> userPerkDTOs = await _webServerService.GetUserPerks(userId);
            return Ok(userPerkDTOs);
        }
    }
}
