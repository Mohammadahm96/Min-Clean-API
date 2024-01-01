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

        // Hash the user's password
        var (hash, salt) = _passwordHasher.GeneratePasswordHash(request.NewUser.Password);

        // Here, you can use AutoMapper or manual mapping to convert Dto to Domain Model
        var userToCreate = new UserModel
        {
            Id = Guid.NewGuid(),
            Username = request.NewUser.UserName,
            Userpassword = hash
        };

        // Add user to the repository
        await _userRepository.AddUser(userToCreate);

        return userToCreate;
    }
}
