using DataTransferObjects.ProductDTOs;
using Domain.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDTO productDTO)
        {
            if (await _productService.CreateProduct(productDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(UpdateProductDTO productDTO)
        {
            if (await _productService.UpdateProduct(productDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(Guid productId)
        {
            if (await _productService.DeleteProduct(productId))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> GetProduct(Guid productId)
        {
            GetProductDTO productDTO = await _productService.GetProduct(productId);
            return Ok(productDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductDTO>> GetProducts()
        {
            List<GetProductDTO> productDTOs = await _productService.GetProducts();
            return Ok(productDTOs);
        }

        [HttpGet]
        [Route("Available")]
        public async Task<ActionResult<GetAvailableProductDTO>> GetAvailableProducts()
        {
            List<GetAvailableProductDTO> productDTOs = await _productService.GetAvailableProducts();
            return Ok(productDTOs);
        }
    }
}
