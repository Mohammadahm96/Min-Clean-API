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
                LikesToPlay = request.NewCat.LikesToPlay
            };

            _dbContext.Cats.Add(newCat);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return newCat;
        }
    }
}


