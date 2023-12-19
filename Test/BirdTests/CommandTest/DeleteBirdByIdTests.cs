using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Application.Commands.Birds.DeleteBirds;
using Infrastructure.Database;

namespace Test.BirdTests.DeleteBirdTests
{
    [TestFixture]
    public class DeleteBirdByIdTests
    {
        private DeleteBirdCommandHandler _handler;
        private CleanApiMainContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _dbContext = new CleanApiMainContext();
            _handler = new DeleteBirdCommandHandler(_dbContext);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsSuccess()
        {
            // Arrange
            var birdId = new Guid("12345678-1234-5678-1234-567812345681");

            var deleteCommand = new DeleteBirdCommand { BirdId = birdId };

            // Act
            var result = await _handler.Handle(deleteCommand, default);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidBirdId = Guid.NewGuid();

            var deleteCommand = new DeleteBirdCommand { BirdId = invalidBirdId };

            // Act
            var result = await _handler.Handle(deleteCommand, default);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }
    }
}



