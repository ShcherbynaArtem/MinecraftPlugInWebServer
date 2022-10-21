using FluentValidation;

namespace DataTransferObjects.BundleDTOs
{
    public class UpdateBundleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Discount { get; set; }
        public IEnumerable<Guid> ProductIds { get; set; }
    }

    public class UpdateBundleDTOValidator : AbstractValidator<UpdateBundleDTO>
    {
        public UpdateBundleDTOValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
            RuleForEach(x => x.ProductIds).NotEqual(Guid.Empty);
        }
    }
}
