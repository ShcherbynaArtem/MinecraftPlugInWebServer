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
        public async Task<ActionResult> CreateProduct(CreateProductDTO productDTO)
        {
            if (await _appServerService.CreateProduct(productDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(UpdateProductDTO productDTO)
        {
            if (await _appServerService.UpdateProduct(productDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            if (await _appServerService.DeleteProduct(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> GetProduct(Guid id)
        {
            GetProductDTO productDTO = await _appServerService.GetProduct(id);
            return Ok(productDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductDTO>> GetProducts()
        {
            List<GetProductDTO> productDTOs = await _appServerService.GetProducts();
            return Ok(productDTOs);
        }
    }
}
