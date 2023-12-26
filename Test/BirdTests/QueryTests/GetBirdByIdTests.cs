using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Application.Queries.Birds.GetBirdById;
using Infrastructure.Database;

namespace Test.BirdTests.GetBirdByIdTests
{
    [TestFixture]
    public class GetBirdByIdTests
    {
        private GetBirdByIdQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            //_handler = new GetBirdByIdQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectBird()
        {
            // Arrange
            var birdId = new Guid("12345678-1234-5678-1234-567812345680");

            var query = new GetBirdByIdQuery(birdId);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(birdId, Is.EqualTo(result.Id));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidBirdId = Guid.NewGuid();

            var query = new GetBirdByIdQuery(invalidBirdId);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.IsNull(result);
        }
    }
}

