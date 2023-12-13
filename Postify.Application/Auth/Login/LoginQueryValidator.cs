using FluentValidation;

namespace Postify.Application.Auth.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.EmailOrUsername)
                .NotEmpty().WithMessage("Your email or username is required.");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Your password is required.");
        }
    }
}