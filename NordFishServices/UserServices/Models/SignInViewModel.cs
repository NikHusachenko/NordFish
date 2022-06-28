using FluentValidation;

namespace NordFish.Services.UserServices.Models
{
    public class SignInViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInValidator : AbstractValidator<SignInViewModel>
    {
        public SignInValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().Length(4, 31);
            RuleFor(x => x.Password).NotEmpty().NotNull().Length(3, 31);
        }
    }
}