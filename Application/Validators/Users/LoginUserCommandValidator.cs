using FluentValidation;
using Application.Commands.Users.Login;

namespace Application.Validators.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.LoginUser.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.LoginUser.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}

