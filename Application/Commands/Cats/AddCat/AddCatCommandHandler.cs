using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;
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
                LikesToPlay = request.NewCat.LikesToPlay,
                Breed = request.NewCat.Breed, // Add breed property
                Weight = request.NewCat.Weight // Add weight property
            };

            _dbContext.Cats.Add(newCat);

            // Create ownership relationship
            var ownership = new Ownership
            {
                UserId = request.UserId, // Set the user id
                AnimalId = newCat.Id
            };

            _dbContext.Ownerships.Add(ownership);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return newCat;
        }
    }
}


