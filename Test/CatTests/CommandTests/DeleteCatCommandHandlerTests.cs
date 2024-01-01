using Application.Commands.Cats.DeleteCats;
using Domain.Models;
using Moq;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class DeleteCatCommandHandlerTests
{
    [Test]
    public async Task DeleteCatCommandHandler_ShouldDeleteCat()
    {
        // Arrange
        var catRepositoryMock = new Mock<ICatRepository>();
        var loggerMock = new Mock<ILogger<DeleteCatCommandHandler>>();

        var handler = new DeleteCatCommandHandler(catRepositoryMock.Object, loggerMock.Object);

        var catId = Guid.NewGuid();
        var command = new DeleteCatCommand { CatId = catId };

        // Setup the repository mock to return a cat when GetCatById is called
        catRepositoryMock.Setup(repo => repo.GetCatById(catId))
                          .ReturnsAsync(new Cat { Id = catId, Name = "SampleCat" });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.IsSuccess, Is.EqualTo(true));

        // Verify that the repository method was called
        catRepositoryMock.Verify(repo => repo.GetCatById(catId), Times.Once);
        catRepositoryMock.Verify(repo => repo.DeleteCat(It.IsAny<Cat>()), Times.Once);
    }
}
