using AutoMapper;
using DataAccess;
using DataTransferObjects;
using Entities;

namespace Domain.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IWebServerRepository _webServerRepo;
        public DepartmentService(IWebServerRepository appServerRepo, IMapper mapper)
        {
            _webServerRepo = appServerRepo ?? throw new ArgumentNullException(nameof(appServerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            DepartmentEntity departmentEntity = _mapper.Map<DepartmentEntity>(departmentDTO);
            int rowsAffected = await _webServerRepo.CreateDepartment(departmentEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetDepartmentDTO> GetDepartment(int departmentId)
        {
            DepartmentEntity departmentEntity = await _webServerRepo.GetDepartmentById(departmentId);
            if (!string.IsNullOrEmpty(departmentEntity.Name))
            {
                GetDepartmentDTO departmentDTO = _mapper.Map<GetDepartmentDTO>(departmentEntity);
                return departmentDTO;
            }
            return new GetDepartmentDTO();
        }

        public async Task<bool> UpdateDepartment(UpdateDepartmentDTO departmentDTO)
        {
            DepartmentEntity departmentEntity = _mapper.Map<DepartmentEntity>(departmentDTO);
            int rowsAffected = await _webServerRepo.UpdateDepartment(departmentEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            int rowsAffected = await _webServerRepo.DeleteDepartment(departmentId);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<List<GetDepartmentDTO>> GetDepartments()
        {
            List<DepartmentEntity> departmentEntities = await _webServerRepo.GetDepartments();
            List<GetDepartmentDTO> departmentDTOs = new List<GetDepartmentDTO>();
            foreach (DepartmentEntity departmentEntity in departmentEntities)
            {
                departmentDTOs.Add(_mapper.Map<GetDepartmentDTO>(departmentEntity));
            }
            return departmentDTOs;
        }
    }
}
