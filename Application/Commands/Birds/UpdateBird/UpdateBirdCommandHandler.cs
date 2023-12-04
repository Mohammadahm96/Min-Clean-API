using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdCommandHandler : IRequestHandler<UpdateBirdCommand, Bird>
    {
        private readonly MockDatabase _mockDatabase;

        public UpdateBirdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Bird> Handle(UpdateBirdCommand request, CancellationToken cancellationToken)
        {
            var birdToUpdate = _mockDatabase.Birds.FirstOrDefault(c => c.Id == request.Id);

            if (birdToUpdate != null)
            {
                birdToUpdate.Name = request.UpdatedBird.Name;
                birdToUpdate.CanFly = request.UpdatedBird.CanFly;
            }
            return Task.FromResult(birdToUpdate);
        }
       
    }
}

