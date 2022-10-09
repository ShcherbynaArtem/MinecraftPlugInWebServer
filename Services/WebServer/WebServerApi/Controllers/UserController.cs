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
        public async Task<ActionResult> CreateUser(CreateUserDTO userDTO)
        {
            if (await _appServerService.CreateUser(userDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserDTO userDTO)
        {
            if (await _appServerService.UpdateUser(userDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            if (await _appServerService.DeleteUser(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<GetUserDTO>> GetUser(Guid id)
        {
            GetUserDTO userDTO = await _appServerService.GetUser(id);
            return Ok(userDTO);
        }
    }
}
