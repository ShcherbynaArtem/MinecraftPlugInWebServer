using FluentValidation;

namespace DataTransferObjects.ProductTypeDTOs
{
    public class CreateProductTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Department { get; set; }
    }

    public class CreateProductTypeDTOValidator : AbstractValidator<CreateProductTypeDTO>
    {
        public CreateProductTypeDTOValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Department).GreaterThan(0);
        }
    }
}
