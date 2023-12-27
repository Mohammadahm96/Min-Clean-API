using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly CleanApiMainContext _dbContext;

        public UpdateDogByIdCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            // Implement logic to update the dog in the database based on the command
            var dogToUpdate = await _dbContext.Dogs.FirstOrDefaultAsync(dog => dog.Id == request.Id);

            if (dogToUpdate != null)
            {
                dogToUpdate.Name = request.UpdatedDog.Name;
                dogToUpdate.Breed = request.UpdatedDog.Breed;
                dogToUpdate.Weight = request.UpdatedDog.Weight;

                await _dbContext.SaveChangesAsync();
            }

            return dogToUpdate;
        }
    }
}