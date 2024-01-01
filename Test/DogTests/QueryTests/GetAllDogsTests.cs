using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Moq;
using NUnit.Framework;

[TestFixture]
public class GetAllDogsQueryHandlerTests
{
    [Test]
    public async Task Handle_ShouldReturnListOfDogs()
    {
        // Arrange
        var mockDogRepository = new Mock<IDogRepository>();
        var dogsFromRepository = new List<Dog>
        {
            new Dog { Id = Guid.NewGuid(), Name = "Buddy", Breed = "Labrador", Weight = 30 },
            new Dog { Id = Guid.NewGuid(), Name = "Max", Breed = "Golden Retriever", Weight = 25 }
            // Add more sample dogs as needed
        };

        mockDogRepository.Setup(repo => repo.GetAllDogs()).ReturnsAsync(dogsFromRepository);

        var getAllDogsQuery = new GetAllDogsQuery();
        var handler = new GetAllDogsQueryHandler(mockDogRepository.Object);

        // Act
        var result = await handler.Handle(getAllDogsQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsInstanceOf<List<Dog>>(result);
        Assert.That(result.Count, Is.EqualTo(dogsFromRepository.Count));
    }
}
