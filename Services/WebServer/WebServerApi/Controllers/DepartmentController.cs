using DataTransferObjects;
using Domain.DepartmentService;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
        }

        [HttpPost]
        public async Task<ActionResult> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            if(await _departmentService.CreateDepartment(departmentDTO))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDepartment(UpdateDepartmentDTO departmentDTO)
        {
            if(await _departmentService.UpdateDepartment(departmentDTO))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDepartment(int departmentId)
        {
            if(await _departmentService.DeleteDepartment(departmentId))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetDepartmentDTO>> GetDepartment(int departmentId)
        {
            GetDepartmentDTO departmentDTO = await _departmentService.GetDepartment(departmentId);
            return Ok(departmentDTO);
        }

        [HttpGet]
        public async Task<ActionResult<GetDepartmentDTO>> GetDepartments()
        {
            List<GetDepartmentDTO> departmentDTOs = await _departmentService.GetDepartments();
            return Ok(departmentDTOs);
        }
    }
}
