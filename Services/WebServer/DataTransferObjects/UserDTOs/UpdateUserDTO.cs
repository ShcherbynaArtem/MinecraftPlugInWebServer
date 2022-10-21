using FluentValidation;

namespace DataTransferObjects.UserDTOs
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public int? Lives { get; set; }
    }

    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(1, 20);
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Ip).Matches(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$");
        }
    }
}
