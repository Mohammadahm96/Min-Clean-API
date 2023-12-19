using NUnit.Framework;
using Application.Queries.Cats.GetCatById;
using Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.CatTests.QueryTests
{
    [TestFixture]
    public class GetCatByIdQueryHandlerTests
    {
        private GetCatByIdQueryHandler _handler;
        private CleanApiMainContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _dbContext = new CleanApiMainContext();
            _handler = new GetCatByIdQueryHandler(_dbContext);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectCat()
        {
            // Arrange
            var catId = new Guid("12345678-1234-5678-1234-567812345675");

            var query = new GetCatByIdQuery(catId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(catId));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidCatId = Guid.NewGuid();

            var query = new GetCatByIdQuery(invalidCatId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}

