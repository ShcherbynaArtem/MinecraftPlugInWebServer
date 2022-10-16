using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IWebServerService _webServerService;

        public ProductTypeController(IWebServerService webServerService)
        {
            _webServerService = webServerService ?? throw new ArgumentNullException(nameof(webServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductType(CreateProductTypeDTO bundleDTO)
        {
            if (await _webServerService.CreateProductType(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductType(UpdateProductTypeDTO productTypeDTO)
        {
            if (await _webServerService.UpdateProductType(productTypeDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProductType(int productTypeId)
        {
            if (await _webServerService.DeleteProductType(productTypeId))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductTypeDTO>> GetProductType(int productTypeId)
        {
            GetProductTypeDTO bundleDTO = await _webServerService.GetProductType(productTypeId);
            return Ok(bundleDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductTypeDTO>> GetProductTypes()
        {
            List<GetProductTypeDTO> bundleDTOs = await _webServerService.GetProductTypes();
            return Ok(bundleDTOs);
        }
    }
}
