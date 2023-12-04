using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Application.Commands.Cats.DeleteCats;
using Infrastructure.Database;

namespace Test.CatTests.DeleteCatTests
{
    [TestFixture]
    public class DeleteCatByIdTests
    {
        private DeleteCatCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteCatCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsSuccess()
        {
            // Arrange
            var catId = new Guid("12345678-1234-5678-1234-567812345679");

            var deleteCommand = new DeleteCatCommand { CatId = catId };

            // Act
            var result = await _handler.Handle(deleteCommand, default);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidCatId = Guid.NewGuid();

            var deleteCommand = new DeleteCatCommand { CatId = invalidCatId };

            // Act
            var result = await _handler.Handle(deleteCommand, default);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }
    }
}

