using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.User;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Cats
{
    internal sealed class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly CleanApiMainContext _dbContext;

        public AddCatCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            var newCat = new Cat
            {
                Id = Guid.NewGuid(),
                Name = request.NewCat.Name,
                LikesToPlay = request.NewCat.LikesToPlay
            };

            _dbContext.Cats.Add(newCat);
            await _dbContext.SaveChangesAsync();

            // Associate the cat with the user
            await AssociateCatWithUser(request.UserId, newCat.Id);

            return newCat;
        }

        private async Task AssociateCatWithUser(Guid userId, Guid catId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user != null)
            {
                var userAnimal = new UserAnimal { UserId = userId, CatId = catId };
                _dbContext.UsersAnimals.Add(userAnimal);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
