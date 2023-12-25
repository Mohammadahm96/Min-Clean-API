using MediatR;
using Infrastructure.Database;


namespace Application.Commands.Cats.DeleteCats
{
    public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, DeleteCatResult>
    {
        private readonly MockDatabase _mockDatabase;

        public DeleteCatCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public async Task<DeleteCatResult> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catToDelete = _mockDatabase.Cats.FirstOrDefault(c => c.Id == request.CatId);

            if (catToDelete != null)
            {
                _mockDatabase.Cats.Remove(catToDelete);
                return new DeleteCatResult { IsSuccess = true };
            }

            return new DeleteCatResult { IsSuccess = false };
        }
    }
}

