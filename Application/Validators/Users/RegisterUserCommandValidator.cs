using Application.Commands.Users.RegisterUser;
using FluentValidation;

namespace Application.Validators.User
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(command => command.NewUser.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(command => command.NewUser.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}

