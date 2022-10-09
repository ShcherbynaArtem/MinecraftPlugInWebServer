using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BundleController : ControllerBase
    {
        private readonly IWebServerService _appServerService;

        public BundleController(IWebServerService appServerService)
        {
            _appServerService = appServerService ?? throw new ArgumentNullException(nameof(appServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateBundle(CreateBundleDTO bundleDTO)
        {
            if (await _appServerService.CreateBundle(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBundle(UpdateBundleDTO bundleDTO)
        {
            if(await _appServerService.UpdateBundle(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBundle(Guid id)
        {
            if (await _appServerService.DeleteBundle(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBundleDTO>> GetBundle(Guid id)
        {
            GetBundleDTO bundleDTO = await _appServerService.GetBundle(id);
            return Ok(bundleDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetBundleDTO>> GetBundles()
        {
            List<GetBundleDTO> bundleDTOs = await _appServerService.GetBundles();
            return Ok(bundleDTOs);
        }
    }
}
