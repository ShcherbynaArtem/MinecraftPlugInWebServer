using DataTransferObjects;
using Domain;
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
        public async Task<ActionResult<bool>> AddProductToBundle(BundleProductDTO bundleProductDTO)
        {
            bool isProductAdded = await _appServerService.AddProductToBundle(bundleProductDTO);
            if (isProductAdded)
                return Ok(isProductAdded);
            return BadRequest(isProductAdded);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProductFromBundle(BundleProductDTO bundleProductDTO)
        {
            bool isProductDeleted = await _appServerService.DeleteProductFromBundle(bundleProductDTO);
            if (isProductDeleted)
            {
                return Ok(isProductDeleted);
            }
            return BadRequest(isProductDeleted);
        }
    }
}
