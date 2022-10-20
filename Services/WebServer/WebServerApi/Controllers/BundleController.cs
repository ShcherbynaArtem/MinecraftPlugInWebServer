using DataTransferObjects;
using Domain.BundleService;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BundleController : ControllerBase
    {
        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService ?? throw new ArgumentNullException(nameof(bundleService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateBundle(CreateBundleDTO bundleDTO)
        {
            if (await _bundleService.CreateBundle(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBundle(UpdateBundleDTO bundleDTO)
        {
            if(await _bundleService.UpdateBundle(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBundle(Guid bundleId)
        {
            if (await _bundleService.DeleteBundle(bundleId))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBundleDTO>> GetBundle(Guid bundleId)
        {
            GetBundleDTO bundleDTO = await _bundleService.GetBundle(bundleId);
            return Ok(bundleDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetBundleDTO>> GetBundles()
        {
            List<GetBundleDTO> bundleDTOs = await _bundleService.GetBundles();
            return Ok(bundleDTOs);
        }
    }
}
