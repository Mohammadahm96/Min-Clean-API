using System;
using System.Threading;
using System.Threading.Tasks;
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
                CanFly = request.NewBird.CanFly
            };

            // Add bird to the database context
            _dbContext.Birds.Add(newBird);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return newBird;
        }
    }
}
