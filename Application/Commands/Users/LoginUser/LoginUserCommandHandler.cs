using Application.Commands.Users.Login;
using Application.Exceptions;
using Application.Validators.User;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly CleanApiMainContext _dbContext;
    private readonly LoginUserCommandValidator _Validator;

    public LoginUserCommandHandler(CleanApiMainContext dbContext, LoginUserCommandValidator validator)
    {
        _dbContext = dbContext;
        _Validator = validator;
    }

    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _Validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationException(string.Join("; ", errorMessages));
        }

        var user = await _dbContext.Users.SingleOrDefaultAsync(u =>
            u.Username == request.LoginUser.UserName && u.Userpassword == request.LoginUser.Password);

        if (user == null)
        {
            // User not found or invalid credentials
            throw new UserNotFoundException(request.LoginUser.UserName);
        }

        // For simplicity, you can create a simple response class to hold both the token and user ID
        return new LoginResponse
        {
            Token = true, // Replace this with your actual token generation logic
            UserId = user.Id
        };
    }
}
