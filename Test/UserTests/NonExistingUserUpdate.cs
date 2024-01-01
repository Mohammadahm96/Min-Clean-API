using Application.Dtos;
using Application.Exceptions;
using Domain.Models.User;
using Moq;

[TestFixture]
public class NonExistingUserUpdate
{
    [Test]
    public void Handle_NonExistingUser_ThrowsUserIdNotFoundException()
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
            .ReturnsAsync((UserModel)null);

        // Act and Assert
        Assert.ThrowsAsync<UserIdNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}