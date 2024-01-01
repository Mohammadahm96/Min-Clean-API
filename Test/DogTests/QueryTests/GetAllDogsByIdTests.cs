using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Dogs.GetById;
using Domain.Models;
using Moq;
using NUnit.Framework;

[TestFixture]
public class GetDogByIdQueryHandlerTests
{
    [Test]
    public async Task Handle_ShouldReturnDogById()
    {
        // Arrange
        var mockDogRepository = new Mock<IDogRepository>();
        var dogId = Guid.NewGuid();
        var dogFromRepository = new Dog { Id = dogId, Name = "Buddy", Breed = "Labrador", Weight = 30 };

        mockDogRepository.Setup(repo => repo.GetDogById(dogId)).ReturnsAsync(dogFromRepository);

        var getDogByIdQuery = new GetDogByIdQuery(dogId);
        var handler = new GetDogByIdQueryHandler(mockDogRepository.Object);

        // Act
        var result = await handler.Handle(getDogByIdQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsInstanceOf<Dog>(result);
        Assert.That(result, Is.EqualTo(dogFromRepository));
    }
}
