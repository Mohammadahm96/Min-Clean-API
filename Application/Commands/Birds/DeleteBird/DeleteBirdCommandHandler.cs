using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Birds.DeleteBirds
{
    public class DeleteBirdCommandHandler : IRequestHandler<DeleteBirdCommand, DeleteBirdResult>
    {
        private readonly MockDatabase _mockDatabase;

        public DeleteBirdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public async Task<DeleteBirdResult> Handle(DeleteBirdCommand request, CancellationToken cancellationToken)
        {
            var birdToDelete = _mockDatabase.Birds.FirstOrDefault(b => b.Id == request.BirdId);

            if (birdToDelete != null)
            {
                _mockDatabase.Birds.Remove(birdToDelete);
                return new DeleteBirdResult { IsSuccess = true };
            }

            return new DeleteBirdResult { IsSuccess = false };
        }
    }
}


