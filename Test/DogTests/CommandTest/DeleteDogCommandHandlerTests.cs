using Application.Commands.Dogs.DeleteDogs;
using Domain.Models;
using Moq;

[TestFixture]
public class DeleteDogCommandHandlerTests
{
    [Test]
    public async Task Handle_ShouldDeleteDog()
    {
        // Arrange
        var mockDogRepository = new Mock<IDogRepository>();
        var dogToDelete = new Dog { Id = Guid.NewGuid(), Name = "Buddy", Breed = "Labrador", Weight = 30 };
        var deleteDogCommand = new DeleteDogCommand { DogId = dogToDelete.Id };

        mockDogRepository.Setup(repo => repo.GetDogById(dogToDelete.Id)).ReturnsAsync(dogToDelete);

        var handler = new DeleteDogCommandHandler(mockDogRepository.Object);

        // Act
        var result = await handler.Handle(deleteDogCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }
}