using Domain;
using DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BundleProductController : ControllerBase
    {
        private readonly IWebServerService _appServerService;

        public BundleProductController(IWebServerService appServerService)
        {
            _appServerService = appServerService ?? throw new ArgumentNullException(nameof(appServerService));
        }

        [HttpPost]
        public async Task<ActionResult> AddProductToBundle(CreateBundleProductDTO bundleProductDTO)
        {
            if (await _appServerService.AddProductToBundle(bundleProductDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProductFromBundle(Guid id)
        {
            if (await _appServerService.DeleteProductFromBundle(id))
                return Ok();
            return BadRequest();
        }
    }
}
