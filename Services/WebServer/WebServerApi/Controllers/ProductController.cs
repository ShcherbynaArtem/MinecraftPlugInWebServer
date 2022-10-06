using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IWebServerService _appServerService;

        public ProductController(IWebServerService appServerService)
        {
            _appServerService = appServerService ?? throw new ArgumentNullException(nameof(appServerService));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateProduct(ProductDTO productDTO)
        {
            bool isProductCreated = await _appServerService.CreateProduct(productDTO);
            if (isProductCreated)
                return Ok(isProductCreated);
            return BadRequest(isProductCreated);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProduct(ProductDTO productDTO)
        {
            bool isProductUpdated = await _appServerService.UpdateProduct(productDTO);
            if (isProductUpdated)
                return Ok(isProductUpdated);
            return BadRequest(isProductUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct(Guid id)
        {
            bool isProductDeleted = await _appServerService.DeleteProduct(id);
            if (isProductDeleted)
            {
                return Ok(isProductDeleted);
            }
            return BadRequest(isProductDeleted);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        {
            ProductDTO productDTO = await _appServerService.GetProduct(id);
            return Ok(productDTO);
        }

        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetProducts()
        {
            List<ProductDTO> productDTOs = await _appServerService.GetProducts();
            return Ok(productDTOs);
        }
    }
}
