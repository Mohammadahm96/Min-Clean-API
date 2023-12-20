using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.User;
using Infrastructure.Database;
using MediatR;

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
            Dog dogToCreate = new Dog
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };

            _dbContext.Dogs.Add(dogToCreate);
            await _dbContext.SaveChangesAsync();

            // Associate the dog with the user
            await AssociateDogWithUser(request.UserId, dogToCreate.Id);

            return dogToCreate;
        }

        private async Task AssociateDogWithUser(Guid userId, Guid dogId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user != null)
            {
                var userAnimal = new UserAnimal { UserId = userId, DogId = dogId };
                _dbContext.UsersAnimals.Add(userAnimal);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
