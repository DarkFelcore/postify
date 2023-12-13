using FluentValidation;

namespace Postify.Application.Auth.Register
{
    public class RegsiterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private readonly string _emailRegex = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        public RegsiterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Your first name is required")
                .Custom((x, context) =>
                {
                    if (int.TryParse(x, out int value))
                    {
                        context.AddFailure($"Your first name cannot contain numbers");
                    }
                })
                .MinimumLength(2).WithMessage("Your first name must be at least 2 characters long");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Your last name is required")
                .Custom((x, context) =>
                {
                    if (int.TryParse(x, out int value))
                    {
                        context.AddFailure($"Your last name cannot contain numbers");
                    }
                })
                .MinimumLength(2).WithMessage("Your last name must be at least 2 characters long");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Your username is required")
                .MinimumLength(5).WithMessage("Your username must be at least 5 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Your email address is required.")
                .Matches(_emailRegex).WithMessage("Enter a valid email address.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Your password is required.")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}