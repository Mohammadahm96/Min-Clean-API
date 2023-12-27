// DeleteUserCommand
using Application.Exceptions;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.Delete
{
    public class DeleteUserCommand : IRequest<DeleteUserResult>
    {
        public Guid UserId { get; }

        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
