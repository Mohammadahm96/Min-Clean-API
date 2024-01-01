using Application.Commands.Cats;
using Application.Dtos;
using Microsoft.Extensions.Logging;
using Moq;
using Domain.Models;

[TestFixture]
public class AddCatCommandHandlerTests
{
    [Test]
    public async Task AddCatCommandHandler_ShouldAddCat()
    {
        // Arrange
        var catRepositoryMock = new Mock<ICatRepository>();
        var loggerMock = new Mock<ILogger<AddCatCommandHandler>>();
        var handler = new AddCatCommandHandler(catRepositoryMock.Object, loggerMock.Object);

        var userId = Guid.NewGuid();
        var command = new AddCatCommand(new CatDto { Name = "Fluffy", LikesToPlay = true, Breed = "Persian", Weight = 5 }, userId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        catRepositoryMock.Verify(repo => repo.AddCat(It.IsAny<Cat>(), userId), Times.Once);
    }
}
