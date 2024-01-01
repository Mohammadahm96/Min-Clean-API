using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdCommandHandler : IRequestHandler<UpdateBirdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ILogger<UpdateBirdCommandHandler> _logger;

        public UpdateBirdCommandHandler(IBirdRepository birdRepository, ILogger<UpdateBirdCommandHandler> logger)
        {
            _birdRepository = birdRepository;
            _logger = logger;
        }

        public async Task<Bird> Handle(UpdateBirdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var birdToUpdate = await _birdRepository.GetBirdById(request.Id);

                if (birdToUpdate != null)
                {
                    birdToUpdate.Name = request.UpdatedBird.Name;
                    birdToUpdate.CanFly = request.UpdatedBird.CanFly;
                    birdToUpdate.Color = request.UpdatedBird.Color;

                    await _birdRepository.UpdateBird(birdToUpdate);
                }

                return birdToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bird");
                throw;
            }
        }
    }
}

