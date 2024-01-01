using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.Delete;
using Application.Commands.Users.DeleteUser;
using Application.Exceptions;
using Domain.Models.User;
using Infrastructure.Database;
using Moq;
using NUnit.Framework;

[TestFixture]
public class DeleteUserCommandHandlerTests
{
    [Test]
    public async Task Handle_UserExists_DeletesUserSuccessfully()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var handler = new DeleteUserCommandHandler(userRepositoryMock.Object);

        var userId = Guid.NewGuid();
        var command = new DeleteUserCommand(userId);

        userRepositoryMock.Setup(u => u.GetUserById(It.IsAny<Guid>()))
            .ReturnsAsync(new UserModel { Id = userId });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(result.Message, Is.EqualTo("User deleted successfully."));

        userRepositoryMock.Verify(u => u.DeleteUserAsync(It.IsAny<UserModel>()), Times.Once);
    }
}
