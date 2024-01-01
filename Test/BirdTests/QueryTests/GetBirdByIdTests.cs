using Application.Queries.Birds.GetAllBirds;
using Application.Queries.Birds.GetBirdById;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[TestFixture]
public class GetBirdByIdTests
{
    [Test]
    public async Task GetBirdByIdQueryHandler_ShouldReturnBirdById()
    {
        // Arrange
        var birdRepositoryMock = new Mock<IBirdRepository>();
        var handler = new GetBirdByIdQueryHandler(birdRepositoryMock.Object);

        var birdId = Guid.NewGuid();
        var query = new GetBirdByIdQuery(birdId);

        var expectedBird = new Bird { Id = birdId, Name = "TestBird", CanFly = true, Color = "Blue" };
        birdRepositoryMock.Setup(repo => repo.GetBirdById(birdId)).ReturnsAsync(expectedBird);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Id, Is.EqualTo(expectedBird.Id));
        Assert.That(result.Name, Is.EqualTo(expectedBird.Name));
        Assert.That(result.CanFly, Is.EqualTo(expectedBird.CanFly));
        Assert.That(result.Color, Is.EqualTo(expectedBird.Color));

        // Verify that the repository method was called
        birdRepositoryMock.Verify(repo => repo.GetBirdById(birdId), Times.Once);
    }
}
