using Application.Exceptions;
using Domain.Models.User;
using MediatR;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _userRepository.GetUserById(request.UserId);

        if (userToUpdate == null)
        {
            // User not found
            throw new UserIdNotFoundException(request.UserId);
        }

        // Update user properties
        userToUpdate.Username = request.UpdatedUser.UserName;
        userToUpdate.Userpassword = request.UpdatedUser.Password;

        await _userRepository.UpdateUserAsync(userToUpdate);

        return userToUpdate;
    }
}
