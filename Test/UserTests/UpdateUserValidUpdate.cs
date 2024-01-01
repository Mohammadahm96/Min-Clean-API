using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users;
using Application.Dtos;
using Domain.Models.User;
using Infrastructure.Database;
using Moq;
using NUnit.Framework;

[TestFixture]
public class UpdateUserCommandHandlerTests
{
    [Test]
    public async Task Handle_ValidUpdate_ReturnsUpdatedUser()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var handler = new UpdateUserCommandHandler(userRepositoryMock.Object);

        var userId = Guid.NewGuid();
        var updateUserDto = new UserDto
        {
            UserName = "updateduser",
            Password = "updatedpassword"
        };

        var command = new UpdateUserCommand(userId, updateUserDto);

        userRepositoryMock.Setup(u => u.GetUserById(userId))
            .ReturnsAsync(new UserModel
            {
                Id = userId,
                Username = "existinguser",
                Userpassword = "existingpassword"
            });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Id, Is.EqualTo(userId));
        Assert.That(result.Username, Is.EqualTo(updateUserDto.UserName));
        Assert.That(result.Userpassword, Is.EqualTo(updateUserDto.Password));

        userRepositoryMock.Verify(u => u.UpdateUserAsync(It.IsAny<UserModel>()), Times.Once);
    }
}