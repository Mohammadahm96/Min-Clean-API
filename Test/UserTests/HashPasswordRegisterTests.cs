using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users.RegisterUser;
using Application.Dtos;
using Application.Exceptions;
using Application.Validators.User;
using Domain.Models.User;
using Infrastructure.Security;
using Moq;
using NUnit.Framework;

[TestFixture]
public class HashPasswordRegisterTests
{
    [Test]
    public async Task Handle_ValidRegistration_ReturnsUserModel()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var validator = new RegisterUserCommandValidator();

        var handler = new RegisterUserCommandHandler(userRepositoryMock.Object, validator, passwordHasherMock.Object);

        // Initialize a UserDto
        var userDto = new UserDto
        {
            UserName = "testuser",
            Password = "testpassword"
        };

        var command = new RegisterUserCommand(userDto);

        passwordHasherMock.Setup(p => p.GeneratePasswordHash(It.IsAny<string>()))
            .Returns(("hashedpassword", "salt"));

        userRepositoryMock.Setup(u => u.AddUser(It.IsAny<UserModel>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Username, Is.EqualTo(userDto.UserName));
        Assert.That(result.Userpassword, Is.EqualTo("hashedpassword"));

        // Verifying that the AddUser was called with all the correct parameters
        userRepositoryMock.Verify(u => u.AddUser(It.Is<UserModel>(user =>
            user.Username == userDto.UserName &&
            user.Userpassword == "hashedpassword")), Times.Once);
    }
}
