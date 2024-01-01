using Application.Commands.Users.Delete;
using Application.Commands.Users.DeleteUser;
using Application.Exceptions;
using Domain.Models.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[TestFixture]
public class UserNotExistsTests
{
    [Test]
    public void Handle_UserNotExists_ThrowsUserIdNotFoundException()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var handler = new DeleteUserCommandHandler(userRepositoryMock.Object);

        var userId = Guid.NewGuid();
        var command = new DeleteUserCommand(userId);

        userRepositoryMock.Setup(u => u.GetUserById(It.IsAny<Guid>()))
            .ReturnsAsync((UserModel)null);

        // Act and Assert
        Assert.ThrowsAsync<UserIdNotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        userRepositoryMock.Verify(u => u.DeleteUserAsync(It.IsAny<UserModel>()), Times.Never);
    }
}