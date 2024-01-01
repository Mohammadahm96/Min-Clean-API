using Application.Commands.Birds;
using Application.Dtos;
using Infrastructure.Repositories.Birds;
using Microsoft.Extensions.Logging;
using Moq;
using Domain.Models;



[TestFixture]
public class AddBirdCommandHandlerTests
{
    [Test]
    public async Task AddBirdCommandHandler_ShouldAddBird()
    {
        // Arrange
        var birdRepositoryMock = new Mock<IBirdRepository>();
        var loggerMock = new Mock<ILogger<AddBirdCommandHandler>>();
        var handler = new AddBirdCommandHandler(null, birdRepositoryMock.Object, loggerMock.Object);

        var birdDto = new BirdDto
        {
            Name = "TestBird",
            CanFly = true,
            Color = "Blue"
        };

        var command = new AddBirdCommand(birdDto, Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Name, Is.EqualTo(birdDto.Name));
        Assert.That(result.CanFly, Is.EqualTo(birdDto.CanFly));
        Assert.That(result.Color, Is.EqualTo(birdDto.Color));

        // Verify that the repository method was called
        birdRepositoryMock.Verify(repo => repo.AddBird(It.IsAny<Bird>(), It.IsAny<Guid>()), Times.Once);
    }
}
