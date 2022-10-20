using DataTransferObjects.UserItemDTOs;
using Domain.UserItemService;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserItemController : ControllerBase
    {
        private readonly IUserItemService _userItemService;

        public UserItemController(IUserItemService userItemService)
        {
            _userItemService = userItemService ?? throw new ArgumentNullException(nameof(userItemService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserItem(CreateUserItemDTO userItemDTO)
        {
            if (await _userItemService.CreateUserItem(userItemDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> MarkUserItemAsReceived(Guid userItemId)
        {
            if (await _userItemService.MarkUserItemAsReceived(userItemId))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserItem(Guid userItemId)
        {
            if (await _userItemService.DeleteUserItem(userItemId))
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<GetUserItemDTO>> GetUserItems(Guid userId)
        {
            List<GetUserItemDTO> userItemDTOs = await _userItemService.GetUserItems(userId);
            return Ok(userItemDTOs);
        }

        [HttpGet]
        [Route("Available")]
        public async Task<ActionResult<GetNotReceivedItemDTO>> GetNotReceivedUserItems(Guid userId)
        {
            List<GetNotReceivedItemDTO> userItemDTOs = await _userItemService.GetNotReceivedUserItems(userId);
            return Ok(userItemDTOs);
        }
    }
}
