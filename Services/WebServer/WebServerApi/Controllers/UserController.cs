using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IWebServerService _appServerService;

        public UserController(IWebServerService appServerService)
        {
            _appServerService = appServerService ?? throw new ArgumentNullException(nameof(appServerService));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateUser(UserDTO userDTO)
        {
            bool isUserCreated = await _appServerService.CreateUser(userDTO);
            if (isUserCreated)
            {
                return Ok(isUserCreated);
            }
            return BadRequest(isUserCreated);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateUser(UserDTO userDTO)
        {
            bool isUserUpdated = await _appServerService.UpdateUser(userDTO);
            if (isUserUpdated)
            {
                return Ok(isUserUpdated);
            }
            return BadRequest(isUserUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser(Guid id)
        {
            bool isUserDeleted = await _appServerService.DeleteUser(id);
            if (isUserDeleted)
            {
                return Ok(isUserDeleted);
            }
            return BadRequest(isUserDeleted);
        }

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            UserDTO userDTO = await _appServerService.GetUser(id);
            return Ok(userDTO);
        }

        
    }
}
