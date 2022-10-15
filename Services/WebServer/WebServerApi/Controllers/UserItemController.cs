﻿using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserItemController : ControllerBase
    {
        private readonly IWebServerService _webServerService;

        public UserItemController(IWebServerService webServerService)
        {
            _webServerService = webServerService ?? throw new ArgumentNullException(nameof(webServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserItem(CreateUserItemDTO userItemDTO)
        {
            if (await _webServerService.CreateUserItem(userItemDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> MarkUserItemAsReceived(Guid userItemId)
        {
            if (await _webServerService.MarkUserItemAsReceived(userItemId))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserItem(Guid id)
        {
            if (await _webServerService.DeleteUserItem(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<GetUserItemDTO>> GetUserItems(Guid userId)
        {
            List<GetUserItemDTO> userDTO = await _webServerService.GetUserItems(userId);
            return Ok(userDTO);
        }

        [HttpGet]
        [Route("Available")]
        public async Task<ActionResult<GetNotReceivedItemDTO>> GetNotReceivedUserItems(Guid userId)
        {
            List<GetNotReceivedItemDTO> userDTO = await _webServerService.GetNotReceivedUserItems(userId);
            return Ok(userDTO);
        }
    }
}
