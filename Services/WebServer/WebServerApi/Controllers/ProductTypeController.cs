using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IWebServerService _appServerService;

        public ProductTypeController(IWebServerService appServerService)
        {
            _appServerService = appServerService ?? throw new ArgumentNullException(nameof(appServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductType(CreateProductTypeDTO bundleDTO)
        {
            if (await _appServerService.CreateProductType(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductType(UpdateProductTypeDTO bundleDTO)
        {
            if (await _appServerService.UpdateProductType(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            if (await _appServerService.DeleteProductType(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductTypeDTO>> GetProductType(int id)
        {
            GetProductTypeDTO bundleDTO = await _appServerService.GetProductType(id);
            return Ok(bundleDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductTypeDTO>> GetProductTypes()
        {
            List<GetProductTypeDTO> bundleDTOs = await _appServerService.GetProductTypes();
            return Ok(bundleDTOs);
        }
    }
}
