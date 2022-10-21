using FluentValidation;

namespace DataTransferObjects.DepartmentDTOs
{
    public class UpdateDepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateDepartmentDTOValidator : AbstractValidator<UpdateDepartmentDTO>
    {
        public UpdateDepartmentDTOValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
