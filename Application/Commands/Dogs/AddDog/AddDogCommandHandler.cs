using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs
{
    internal sealed class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly CleanApiMainContext _dbContext;

        public AddDogCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };

            // Add the dog to the actual database context
            _dbContext.Dogs.Add(dogToCreate);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return dogToCreate;
        }
    }
}
