using DataTransferObjects;
using Domain;
using Domain.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserDTO userDTO)
        {
            if (await _userService.CreateUser(userDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserDTO userDTO)
        {
            if (await _userService.UpdateUser(userDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            if (await _userService.DeleteUser(userId))
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<GetUserDTO>> GetUser(Guid userId)
        {
            GetUserDTO userDTO = await _userService.GetUser(userId);
            return Ok(userDTO);
        }
    }
}
