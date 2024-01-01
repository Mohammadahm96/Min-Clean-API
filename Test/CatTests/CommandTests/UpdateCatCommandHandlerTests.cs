using Application.Commands.Cats.UpdateCats;
using Application.Dtos;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

[TestFixture]
public class UpdateCatCommandHandlerTests
{
    [Test]
    public async Task UpdateCatCommandHandler_ShouldUpdateCat()
    {
        // Arrange
        var catRepositoryMock = new Mock<ICatRepository>();
        var loggerMock = new Mock<ILogger<UpdateCatCommandHandler>>();
        var handler = new UpdateCatCommandHandler(catRepositoryMock.Object, loggerMock.Object);

        var catId = Guid.NewGuid();
        var command = new UpdateCatCommand(
            new CatDto { Name = "UpdatedFluffy", Breed = "UpdatedPersian", Weight = 6 },
            catId
        );

        var existingCat = new Cat { Id = catId, Name = "Fluffy", Breed = "Persian", Weight = 5 };
        catRepositoryMock.Setup(repo => repo.GetCatById(catId)).ReturnsAsync(existingCat);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Name, Is.EqualTo(command.UpdatedCat.Name));
        Assert.That(result.Breed, Is.EqualTo(command.UpdatedCat.Breed));
        Assert.That(result.Weight, Is.EqualTo(command.UpdatedCat.Weight));

        catRepositoryMock.Verify(repo => repo.UpdateCat(It.IsAny<Cat>()), Times.Once);
    }
}