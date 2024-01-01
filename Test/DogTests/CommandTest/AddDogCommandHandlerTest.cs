using NUnit.Framework;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;

[TestFixture]
public class AddDogCommandHandlerTests
{
    [Test]
    public async Task Handle_AddsNewDogToRepository()
    {
        // Arrange
        var mockDogRepository = new Mock<IDogRepository>();
        var addDogCommand = new AddDogCommand(
            new DogDto { Name = "TestDog", Weight = 20, Breed = "TestBreed" },
            Guid.NewGuid()
        );
        var cancellationToken = new CancellationToken();

        // Act
        var addDogCommandHandler = new AddDogCommandHandler(mockDogRepository.Object);
        await addDogCommandHandler.Handle(addDogCommand, cancellationToken);

        // Assert
        mockDogRepository.Verify(
            repo => repo.AddDog(It.IsAny<Dog>(), It.IsAny<Guid>()),
            Times.Once
        );
    }
}
