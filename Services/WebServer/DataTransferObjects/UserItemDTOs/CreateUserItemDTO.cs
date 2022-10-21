using FluentValidation;

namespace DataTransferObjects.UserItemDTOs
{
    public class CreateUserItemDTO
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
    }

    public class CreateUserItemDTOValidator : AbstractValidator<CreateUserItemDTO>
    {
        public CreateUserItemDTOValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.ItemId).NotEqual(Guid.Empty);
        }
    }
}
