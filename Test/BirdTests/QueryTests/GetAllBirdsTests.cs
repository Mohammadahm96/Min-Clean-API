using Application.Queries.Birds.GetAllBirds;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using Microsoft.Identity.Client;
using Moq;

[TestFixture]
public class GetAllBirdsTests
{
    [Test]
    public async Task GetAllBirdsQueryHandler_ShouldReturnAllBirds()
    {
        // Arrange
        var birdRepositoryMock = new Mock<IBirdRepository>();
        var handler = new GetAllBirdsQueryHandler(birdRepositoryMock.Object);

        var expectedBirds = new List<Bird>
        {
            new Bird { Id = Guid.NewGuid(), Name = "Bird1", CanFly = true, Color = "Red" },
            new Bird { Id = Guid.NewGuid(), Name = "Bird2", CanFly = false, Color = "Green" }
        };

        birdRepositoryMock.Setup(repo => repo.GetAllBirds()).ReturnsAsync(expectedBirds);

        // Act
        var result = await handler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Count, Is.EqualTo(expectedBirds.Count));

        // Verify that the repository method was called
        birdRepositoryMock.Verify(repo => repo.GetAllBirds(), Times.Once);
    }
}

