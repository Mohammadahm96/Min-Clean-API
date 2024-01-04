using Infrastructure.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.DeleteDogs
{
    public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, DeleteDogResult>
    {
        private readonly IDogRepository _dogRepository;

        public DeleteDogCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<DeleteDogResult> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            var dogToDelete = await _dogRepository.GetDogById(request.DogId);

            if (dogToDelete != null)
            {
                await _dogRepository.DeleteDog(dogToDelete);
                return new DeleteDogResult { IsSuccess = true };
            }

            return new DeleteDogResult { IsSuccess = false };
        }
    }
}
