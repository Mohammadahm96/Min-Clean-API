using Application.Commands.Users.RegisterUser;
using Application.Exceptions;
using Application.Validators.User;
using Domain.Models.User;
using Infrastructure.Security;
using MediatR;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserModel>
{
    private readonly IUserRepository _userRepository;
    private readonly RegisterUserCommandValidator _validator;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, RegisterUserCommandValidator validator, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var registerCommandValidation = _validator.Validate(request);

        if (!registerCommandValidation.IsValid)
        {
            var allErrors = registerCommandValidation.Errors.ConvertAll(errors => errors.ErrorMessage);
            throw new ArgumentException("Registration error: " + string.Join("; ", allErrors));
        }

        // Hasar user lösenord
        var (hash, salt) = _passwordHasher.GeneratePasswordHash(request.NewUser.Password);

        var userToCreate = new UserModel
        {
            Id = Guid.NewGuid(),
            Username = request.NewUser.UserName,
            Userpassword = hash
        };

        // Lägger till user till databas
        await _userRepository.AddUser(userToCreate);

        return userToCreate;
    }
}
