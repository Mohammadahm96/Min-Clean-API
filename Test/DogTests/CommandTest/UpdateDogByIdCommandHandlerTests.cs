using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Moq;

[TestFixture]
public class UpdateDogByIdCommandHandlerTests
{
    [Test]
    public async Task Handle_ShouldUpdateDog()
    {
        // Arrange
        var mockDogRepository = new Mock<IDogRepository>();
        var dogToUpdate = new Dog { Id = Guid.NewGuid(), Name = "Buddy", Breed = "Labrador", Weight = 30 };
        var updateDogCommand = new UpdateDogByIdCommand(dogToUpdate.Id, new DogDto { Name = "UpdatedName", Breed = "UpdatedBreed", Weight = 35 });

        mockDogRepository.Setup(repo => repo.GetDogById(dogToUpdate.Id)).ReturnsAsync(dogToUpdate);

        var handler = new UpdateDogByIdCommandHandler(mockDogRepository.Object);

        // Act
        var result = await handler.Handle(updateDogCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Name, Is.EqualTo(updateDogCommand.UpdatedDog.Name));
        Assert.That(result.Breed, Is.EqualTo(updateDogCommand.UpdatedDog.Breed));
        Assert.That(result.Weight, Is.EqualTo(updateDogCommand.UpdatedDog.Weight));
    }
}
