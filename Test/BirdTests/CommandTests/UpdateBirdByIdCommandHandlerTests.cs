using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public class UpdateBirdByIdCommandHandlerTests
{
    [Test]
    public async Task Handle_ShouldUpdateBird()
    {
        // Arrange
        var birdId = Guid.NewGuid();
        var updatedBirdDto = new BirdDto { Name = "UpdatedBird", CanFly = true, Color = "Blue" };

        // Mocking the logger
        var mockLogger = new Mock<ILogger<UpdateBirdCommandHandler>>();

        // Mocking the bird repository
        var mockBirdRepository = new Mock<IBirdRepository>();
        mockBirdRepository.Setup(repo => repo.GetBirdById(birdId))
            .ReturnsAsync(new Bird { Id = birdId, Name = "OriginalBird", CanFly = false, Color = "Red" });

        var handler = new UpdateBirdCommandHandler(mockBirdRepository.Object, mockLogger.Object);

        var command = new UpdateBirdCommand(updatedBirdDto, birdId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Name, Is.EqualTo(updatedBirdDto.Name));
        Assert.That(result.CanFly, Is.EqualTo(updatedBirdDto.CanFly));
        Assert.That(result.Color, Is.EqualTo(updatedBirdDto.Color));
    }
}
