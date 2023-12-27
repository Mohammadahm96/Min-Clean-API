using Application.Exceptions;
using Domain.Models.User;
using Infrastructure.Database;
using MediatR;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
{
    private readonly CleanApiMainContext _dbContext;

    public UpdateUserCommandHandler(CleanApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _dbContext.Users.FindAsync(request.UserId);

        if (userToUpdate == null)
        {
            // User not found
            throw new UserIdNotFoundException(request.UserId);
        }

        // Update user properties
        userToUpdate.Username = request.UpdatedUser.UserName;
        userToUpdate.Userpassword = request.UpdatedUser.Password;

        await _dbContext.SaveChangesAsync();

        return userToUpdate;
    }
}

