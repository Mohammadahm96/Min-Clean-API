using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            // Implement logic to delete the dog from the database based on the command
            var dogToDelete = await _dbContext.Dogs.FirstOrDefaultAsync(d => d.Id == request.DogId);

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
