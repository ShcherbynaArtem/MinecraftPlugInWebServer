using FluentValidation;

namespace DataTransferObjects.BundleDTOs
{
    public class CreateBundleDTO
    {
        public string Name { get; set; }
        public double Discount { get; set; }
        public IEnumerable<Guid> ProductIds { get; set; }
    }

    public class CreateBundleDTOValidator : AbstractValidator<CreateBundleDTO>
    {
        public CreateBundleDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
            RuleForEach(x => x.ProductIds).NotEqual(Guid.Empty);
        }
    }
}
