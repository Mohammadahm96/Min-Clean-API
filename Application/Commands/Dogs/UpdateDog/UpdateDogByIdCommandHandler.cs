using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;

        public UpdateDogByIdCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            var dogToUpdate = await _dogRepository.GetDogById(request.Id);

            if (dogToUpdate != null)
            {
                dogToUpdate.Name = request.UpdatedDog.Name;
                dogToUpdate.Breed = request.UpdatedDog.Breed;
                dogToUpdate.Weight = request.UpdatedDog.Weight;

                await _dogRepository.UpdateDog(dogToUpdate);
            }

            return dogToUpdate;
        }
    }
}
