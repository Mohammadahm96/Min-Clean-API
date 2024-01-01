using Application.Queries.Cats.GetCatById;
using Domain.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class GetCatByIdQueryHandlerTests
{
    [Test]
    public async Task GetCatByIdQueryHandler_ShouldReturnCatById()
    {
        // Arrange
        var catRepositoryMock = new Mock<ICatRepository>();
        var handler = new GetCatByIdQueryHandler(catRepositoryMock.Object);

        var catId = Guid.NewGuid();
        var query = new GetCatByIdQuery(catId);

        var expectedCat = new Cat { Id = catId, Name = "Whiskers", Breed = "Siamese", Weight = 4 };
        catRepositoryMock.Setup(repo => repo.GetCatById(catId)).ReturnsAsync(expectedCat);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result, Is.EqualTo(expectedCat));
        catRepositoryMock.Verify(repo => repo.GetCatById(catId), Times.Once);
    }
}
