using FluentValidation;

namespace DataTransferObjects.DepartmentDTOs
{
    public class CreateDepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateDepartmentDTOValidator : AbstractValidator<CreateDepartmentDTO>
    {
        public CreateDepartmentDTOValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
