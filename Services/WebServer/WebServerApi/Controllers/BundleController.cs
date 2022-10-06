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
        public async Task<ActionResult<bool>> CreateBundle(BundleDTO bundleDTO)
        {
            bool isBundleCreated = await _appServerService.CreateBundle(bundleDTO);
            if (isBundleCreated)
                return Ok(isBundleCreated);
            return BadRequest(isBundleCreated);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateBundle(BundleDTO bundleDTO)
        {
            bool isBundleUpdated = await _appServerService.UpdateBundle(bundleDTO);
            if (isBundleUpdated)
                return Ok(isBundleUpdated);
            return BadRequest(isBundleUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBundle(Guid id)
        {
            bool isBundleDeleted = await _appServerService.DeleteBundle(id);
            if (isBundleDeleted)
            {
                return Ok(isBundleDeleted);
            }
            return BadRequest(isBundleDeleted);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BundleDTO>> GetBundle(Guid id)
        {
            BundleDTO bundleDTO = await _appServerService.GetBundle(id);
            return Ok(bundleDTO);
        }

        [HttpGet]
        public async Task<ActionResult<BundleDTO>> GetBundles()
        {
            List<BundleDTO> bundleDTOs = await _appServerService.GetBundles();
            return Ok(bundleDTOs);
        }


    }
}
