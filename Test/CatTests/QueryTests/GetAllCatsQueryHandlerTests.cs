using Application.Queries.Cats.GetAll;
using Domain.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class GetAllCatsQueryHandlerTests
{
    [Test]
    public async Task GetAllCatsQueryHandler_ShouldReturnAllCats()
    {
        // Arrange
        var catRepositoryMock = new Mock<ICatRepository>();
        var handler = new GetAllCatsQueryHandler(catRepositoryMock.Object);

        var expectedCats = new List<Cat>
        {
            new Cat { Id = Guid.NewGuid(), Name = "Whiskers", Breed = "Siamese", Weight = 4 },
            new Cat { Id = Guid.NewGuid(), Name = "Fluffy", Breed = "Persian", Weight = 5 },
        };

        catRepositoryMock.Setup(repo => repo.GetAllCats()).ReturnsAsync(expectedCats);

        // Act
        var result = await handler.Handle(new GetAllCatsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result, Is.EqualTo(expectedCats));
        catRepositoryMock.Verify(repo => repo.GetAllCats(), Times.Once);
    }
}
