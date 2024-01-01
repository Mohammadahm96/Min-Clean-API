using Application.Commands.Users.Login;
using Application.Dtos;
using Application.Exceptions;
using Application.Validators.User;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly LoginUserCommandValidator _validator;

    public LoginUserCommandHandler(IUserRepository userRepository, LoginUserCommandValidator validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new Exception(string.Join("; ", errorMessages));
        }

        // Check if the user exists
        var user = await _userRepository.GetUserByUsername(request.LoginUser.UserName);
        if (user == null)
        {
            // User not found
            throw new UserNotFoundException(request.LoginUser.UserName);
        }

        // Now check if the password matches
        if (user.Userpassword != request.LoginUser.Password)
        {
            // Invalid password
            throw new UserNotFoundException(request.LoginUser.UserName);
        }

        
        return new LoginResponse
        {
            Token = true, 
            UserId = user.Id
        };
    }
}