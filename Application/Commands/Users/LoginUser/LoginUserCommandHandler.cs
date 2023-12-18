using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Users.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly CleanApiMainContext _dbContext;

        public LoginUserCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == request.LoginUser.UserName);

            if (user != null && user.Userpassword == request.LoginUser.Password)
            {
                // Successful login
                return true;
            }

            // Failed login
            return false;
        }
    }
}
