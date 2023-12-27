// DeleteUserCommandHandler
using Application.Commands.Users.Delete;
using Application.Exceptions;
using Infrastructure.Database;
using MediatR;
using System;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResult>
    {
        private readonly CleanApiMainContext _dbContext;

        public DeleteUserCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = _dbContext.Users.FirstOrDefault(u => u.Id == request.UserId);

            if (userToDelete != null)
            {
                // Remove user from the database context
                _dbContext.Users.Remove(userToDelete);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

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

