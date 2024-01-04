using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.Delete;
using Application.Exceptions;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResult>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetUserById(request.UserId);

            if (userToDelete != null)
            {
                // Tar bort user från databas via repository
                await _userRepository.DeleteUserAsync(userToDelete);

                return new DeleteUserResult
                {
                    IsSuccess = true,
                    Message = "User deleted successfully."
                };
            }

            throw new UserIdNotFoundException(request.UserId);
        }
    }
}
