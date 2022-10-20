using DataTransferObjects.ProductTypeDTOs;
using Domain.ProductTypeService;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService ?? throw new ArgumentNullException(nameof(productTypeService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductType(CreateProductTypeDTO bundleDTO)
        {
            if (await _productTypeService.CreateProductType(bundleDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductType(UpdateProductTypeDTO productTypeDTO)
        {
            if (await _productTypeService.UpdateProductType(productTypeDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProductType(int productTypeId)
        {
            if (await _productTypeService.DeleteProductType(productTypeId))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductTypeDTO>> GetProductType(int productTypeId)
        {
            GetProductTypeDTO bundleDTO = await _productTypeService.GetProductType(productTypeId);
            return Ok(bundleDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductTypeDTO>> GetProductTypes()
        {
            List<GetProductTypeDTO> bundleDTOs = await _productTypeService.GetProductTypes();
            return Ok(bundleDTOs);
        }
    }
}
