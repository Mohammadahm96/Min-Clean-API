using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Dogs.DeleteDogs
{
    public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, DeleteDogResult>
    {
        private readonly CleanApiMainContext _dbContext;

        public DeleteDogCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteDogResult> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            var dogToDelete = _dbContext.Dogs.FirstOrDefault(d => d.Id == request.DogId);

            if (dogToDelete != null)
            {
                _dbContext.Dogs.Remove(dogToDelete);
                await _dbContext.SaveChangesAsync();
                return new DeleteDogResult { IsSuccess = true };
            }

            return new DeleteDogResult { IsSuccess = false };
        }
    }
}



