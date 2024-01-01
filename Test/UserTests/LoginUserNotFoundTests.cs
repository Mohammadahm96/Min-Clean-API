using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.Login;
using Application.Dtos;
using Application.Exceptions;
using Application.Validators.User;
using Domain.Models.User;
using Moq;
using NUnit.Framework;

[TestFixture]
public class LoginUserNotFound
{
    [Test]
    public void Handle_UserNotFound_ThrowsUserNotFoundException()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var validator = new LoginUserCommandValidator();

        var handler = new LoginUserCommandHandler(userRepositoryMock.Object, validator);

        var loginUserDto = new UserDto
        {
            UserName = "nonexistentuser",
            Password = "testpassword"
        };

        var command = new LoginUserCommand(loginUserDto);

        userRepositoryMock.Setup(u => u.GetUserByUsername(It.IsAny<string>()))
            .ReturnsAsync((UserModel)null);

        // Act and Assert
        Assert.ThrowsAsync<UserNotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}
