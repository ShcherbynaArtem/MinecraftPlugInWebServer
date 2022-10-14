using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BundleController : ControllerBase
    {
        private readonly IWebServerService _webServerService;

        public BundleController(IWebServerService webServerService)
        {
            _webServerService = webServerService ?? throw new ArgumentNullException(nameof(webServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateBundle(CreateBundleDTO bundleDTO)
        {
            if (await _webServerService.CreateBundle(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBundle(UpdateBundleDTO bundleDTO)
        {
            if(await _webServerService.UpdateBundle(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBundle(Guid id)
        {
            if (await _webServerService.DeleteBundle(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBundleDTO>> GetBundle(Guid id)
        {
            GetBundleDTO bundleDTO = await _webServerService.GetBundle(id);
            return Ok(bundleDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetBundleDTO>> GetBundles()
        {
            List<GetBundleDTO> bundleDTOs = await _webServerService.GetBundles();
            return Ok(bundleDTOs);
        }
    }
}
