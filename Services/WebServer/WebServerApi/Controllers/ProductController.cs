using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IWebServerService _webServerService;

        public ProductController(IWebServerService webServerService)
        {
            _webServerService = webServerService ?? throw new ArgumentNullException(nameof(webServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDTO productDTO)
        {
            if (await _webServerService.CreateProduct(productDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(UpdateProductDTO productDTO)
        {
            if (await _webServerService.UpdateProduct(productDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(Guid productId)
        {
            if (await _webServerService.DeleteProduct(productId))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> GetProduct(Guid productId)
        {
            GetProductDTO productDTO = await _webServerService.GetProduct(productId);
            return Ok(productDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductDTO>> GetProducts()
        {
            List<GetProductDTO> productDTOs = await _webServerService.GetProducts();
            return Ok(productDTOs);
        }

        [HttpGet]
        [Route("Available")]
        public async Task<ActionResult<GetAvailableProductDTO>> GetAvailableProducts()
        {
            List<GetAvailableProductDTO> productDTOs = await _webServerService.GetAvailableProducts();
            return Ok(productDTOs);
        }
    }
}
