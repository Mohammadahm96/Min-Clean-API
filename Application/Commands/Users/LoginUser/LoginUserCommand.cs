using Application.Dtos;
using MediatR;

namespace Application.Commands.Users.Login
{
    public class LoginUserCommand : IRequest<bool>
    {
        public LoginUserCommand(UserDto loginUser)
        {
            LoginUser = loginUser;
        }

        public UserDto LoginUser { get; }
    }
}