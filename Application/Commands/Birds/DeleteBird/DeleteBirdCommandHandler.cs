using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Repositories.Birds;
using MediatR;

namespace Application.Commands.Birds.DeleteBirds
{
    public class DeleteBirdCommandHandler : IRequestHandler<DeleteBirdCommand, DeleteBirdResult>
    {
        private readonly IBirdRepository _birdRepository;

        public DeleteBirdCommandHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<DeleteBirdResult> Handle(DeleteBirdCommand request, CancellationToken cancellationToken)
        {
            var birdToDelete = await _birdRepository.GetBirdById(request.BirdId);

            if (birdToDelete != null)
            {
                await _birdRepository.DeleteBird(birdToDelete);
                return new DeleteBirdResult { IsSuccess = true };
            }

            return new DeleteBirdResult { IsSuccess = false };
        }
    }
}
