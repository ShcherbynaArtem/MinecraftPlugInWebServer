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
        private readonly IWebServerService _webServerService;

        public UserController(IWebServerService webServerService)
        {
            _webServerService = webServerService ?? throw new ArgumentNullException(nameof(webServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserDTO userDTO)
        {
            if (await _webServerService.CreateUser(userDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserDTO userDTO)
        {
            if (await _webServerService.UpdateUser(userDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            if (await _webServerService.DeleteUser(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<GetUserDTO>> GetUser(Guid id)
        {
            GetUserDTO userDTO = await _webServerService.GetUser(id);
            return Ok(userDTO);
        }
    }
}
