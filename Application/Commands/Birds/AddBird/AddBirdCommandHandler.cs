using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.User;
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
                CanFly = request.NewBird.CanFly
            };

            _dbContext.Birds.Add(newBird);
            await _dbContext.SaveChangesAsync();

            // Associate the bird with the user
            await AssociateBirdWithUser(request.UserId, newBird.Id);

            return newBird;
        }

        private async Task AssociateBirdWithUser(Guid userId, Guid birdId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user != null)
            {
                var userAnimal = new UserAnimal { UserId = userId, BirdId = birdId };
                _dbContext.UsersAnimals.Add(userAnimal);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

