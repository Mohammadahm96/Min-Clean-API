using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Birds
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly CleanApiMainContext _dbContext;

        public AddBirdCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            var newBird = new Bird
            {
                Id = Guid.NewGuid(),
                Name = request.NewBird.Name,
                CanFly = request.NewBird.CanFly,
                Color = request.NewBird.Color // Add color property
            };

            // Add bird to the database
            _dbContext.Birds.Add(newBird);

            // Create ownership relationship
            var ownership = new Ownership
            {
                UserId = request.UserId, // Set the user id
                AnimalId = newBird.Id
            };

            _dbContext.Ownerships.Add(ownership);

            await _dbContext.SaveChangesAsync();

            return newBird;
        }
    }
}


