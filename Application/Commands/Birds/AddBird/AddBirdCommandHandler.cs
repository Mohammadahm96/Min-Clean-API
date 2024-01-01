using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories.Birds;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Birds
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly CleanApiMainContext _dbContext;
        private readonly IBirdRepository _birdRepository;

        private readonly ILogger<AddBirdCommandHandler> _logger;

        public AddBirdCommandHandler(CleanApiMainContext dbContext, IBirdRepository birdRepository, ILogger<AddBirdCommandHandler> logger)
        {
            _dbContext = dbContext;
            _birdRepository = birdRepository;
            _logger = logger;
        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newBird = new Bird
                {
                    Id = Guid.NewGuid(),
                    Name = request.NewBird.Name,
                    CanFly = request.NewBird.CanFly,
                    Color = request.NewBird.Color
                };

                // Add bird to the database using the repository
                await _birdRepository.AddBird(newBird, request.UserId);

                return newBird;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding bird");
                throw new BirdCreationException("Error adding bird.", ex);
            }
        }
    }
}

