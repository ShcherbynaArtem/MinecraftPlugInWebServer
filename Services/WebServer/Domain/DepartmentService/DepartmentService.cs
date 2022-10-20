using AutoMapper;
using DataAccess.DepartmentRepository;
using DataTransferObjects.DepartmentDTOs;
using Entities;

namespace Domain.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            DepartmentEntity departmentEntity = _mapper.Map<DepartmentEntity>(departmentDTO);
            int rowsAffected = await _departmentRepository.CreateDepartment(departmentEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetDepartmentDTO> GetDepartment(int departmentId)
        {
            DepartmentEntity departmentEntity = await _departmentRepository.GetDepartmentById(departmentId);
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
            int rowsAffected = await _departmentRepository.UpdateDepartment(departmentEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            int rowsAffected = await _departmentRepository.DeleteDepartment(departmentId);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<List<GetDepartmentDTO>> GetDepartments()
        {
            List<DepartmentEntity> departmentEntities = await _departmentRepository.GetDepartments();
            List<GetDepartmentDTO> departmentDTOs = new List<GetDepartmentDTO>();
            foreach (DepartmentEntity departmentEntity in departmentEntities)
            {
                departmentDTOs.Add(_mapper.Map<GetDepartmentDTO>(departmentEntity));
            }
            return departmentDTOs;
        }
    }
}
