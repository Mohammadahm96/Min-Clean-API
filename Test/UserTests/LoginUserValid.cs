using Application.Commands.Users.Login;
using Application.Dtos;
using Application.Validators.User;
using Domain.Models.User;
using Moq;

[TestFixture]
public class LoginUserValid
{
    [Test]
    public async Task Handle_ValidLogin_ReturnsLoginResponse()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var validator = new LoginUserCommandValidator();

        var handler = new LoginUserCommandHandler(userRepositoryMock.Object, validator);
        var loginUserDto = new UserDto
        {
            UserName = "testuser",
            Password = "testpassword"
        };

        var command = new LoginUserCommand(loginUserDto);

        // Mock the behavior of userRepository to return a valid user
        userRepositoryMock.Setup(u => u.GetUserByUsername(It.IsAny<string>()))
            .ReturnsAsync(new UserModel
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Userpassword = "testpassword" // Use the actual password for the test
            });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsTrue(result.Token);
        Assert.NotNull(result.UserId);

        userRepositoryMock.Verify(u => u.GetUserByUsername(It.Is<string>(username => username == loginUserDto.UserName)), Times.Once);
    }
}
