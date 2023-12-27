using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdCommandHandler : IRequestHandler<UpdateBirdCommand, Bird>
    {
        private readonly CleanApiMainContext _dbContext;

        public UpdateBirdCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Bird> Handle(UpdateBirdCommand request, CancellationToken cancellationToken)
        {
            var birdToUpdate = _dbContext.Birds.FirstOrDefault(c => c.Id == request.Id);

            if (birdToUpdate != null)
            {
                birdToUpdate.Name = request.UpdatedBird.Name;
                birdToUpdate.CanFly = request.UpdatedBird.CanFly;
                birdToUpdate.Color = request.UpdatedBird.Color;

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }

            return birdToUpdate;
        }
    }
}

