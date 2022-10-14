using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IWebServerService _webServerService;

        public DepartmentController(IWebServerService webServerService)
        {
            _webServerService = webServerService ?? throw new ArgumentNullException(nameof(webServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            if(await _webServerService.CreateDepartment(departmentDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDepartment(UpdateDepartmentDTO departmentDTO)
        {
            if(await _webServerService.UpdateDepartment(departmentDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            if(await _webServerService.DeleteDepartment(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetDepartmentDTO>> GetDepartment(int id)
        {
            GetDepartmentDTO departmentDTO = await _webServerService.GetDepartment(id);
            return Ok(departmentDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetDepartmentDTO>> GetDepartments()
        {
            List<GetDepartmentDTO> departmentDTOs = await _webServerService.GetDepartments();
            return Ok(departmentDTOs);
        }
    }
}
