using FluentValidation;
using System;

namespace DataTransferObjects.UserDTOs
{
    public class CreateUserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public int? Lives { get; set; }
    }

    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(1,20);
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Ip).Matches(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$");
        }
    }
}
