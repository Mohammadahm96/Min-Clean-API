using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Cats.DeleteCats
{
    public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, DeleteCatResult>
    {
        private readonly CleanApiMainContext _dbContext;

        public DeleteCatCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteCatResult> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catToDelete = _dbContext.Cats.FirstOrDefault(c => c.Id == request.CatId);

            if (catToDelete != null)
            {
                _dbContext.Cats.Remove(catToDelete);
                await _dbContext.SaveChangesAsync();
                return new DeleteCatResult { IsSuccess = true };
            }

            return new DeleteCatResult { IsSuccess = false };
        }
    }
}

