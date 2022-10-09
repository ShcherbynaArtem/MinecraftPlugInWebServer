using DataTransferObjects;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IWebServerService _appServerService;

        public DepartmentController(IWebServerService appServerService)
        {
            _appServerService = appServerService ?? throw new ArgumentNullException(nameof(appServerService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            if(await _appServerService.CreateDepartment(departmentDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDepartment(UpdateDepartmentDTO departmentDTO)
        {
            if(await _appServerService.UpdateDepartment(departmentDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            if(await _appServerService.DeleteDepartment(id))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetDepartmentDTO>> GetDepartment(int id)
        {
            GetDepartmentDTO departmentDTO = await _appServerService.GetDepartment(id);
            return Ok(departmentDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetDepartmentDTO>> GetDepartments()
        {
            List<GetDepartmentDTO> departmentDTOs = await _appServerService.GetDepartments();
            return Ok(departmentDTOs);
        }
    }
}
