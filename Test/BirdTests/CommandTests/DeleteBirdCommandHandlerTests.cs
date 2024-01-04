using Application.Commands.Birds.DeleteBirds;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class DeleteBirdCommandHandlerTests
{
    [Test]
    public async Task DeleteBirdCommandHandler_ShouldDeleteBird()
    {
        // Arrange
        var birdRepositoryMock = new Mock<IBirdRepository>();
        var handler = new DeleteBirdCommandHandler(birdRepositoryMock.Object);

        var birdId = Guid.NewGuid();
        var command = new DeleteBirdCommand { BirdId = birdId };

        birdRepositoryMock.Setup(repo => repo.GetBirdById(birdId))
                          .ReturnsAsync(new Bird { Id = birdId, Name = "SampleBird" });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.IsSuccess, Is.EqualTo(true));

        // Verify that the repository method was called
        birdRepositoryMock.Verify(repo => repo.GetBirdById(birdId), Times.Once);
        birdRepositoryMock.Verify(repo => repo.DeleteBird(It.IsAny<Bird>()), Times.Once);
    }
}

