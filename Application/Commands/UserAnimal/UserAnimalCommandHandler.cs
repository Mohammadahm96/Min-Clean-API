using System.Threading;
using System.Threading.Tasks;
using Application.Commands.UserAnimals;
using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Handlers.UserAnimals
{
    public class AssociateUserWithAnimalHandler : IRequestHandler<AssociateUserWithAnimalCommand, bool>
    {
        private readonly IUserAnimalRepository _userAnimalRepository;

        public AssociateUserWithAnimalHandler(IUserAnimalRepository userAnimalRepository)
        {
            _userAnimalRepository = userAnimalRepository;
        }

        public async Task<bool> Handle(AssociateUserWithAnimalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ownership = new Ownership
                {
                    UserId = request.UserAnimal.UserId,
                    AnimalId = request.UserAnimal.AnimalId
                };

                return await _userAnimalRepository.AssociateUserWithAnimal(ownership);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error associating user with animal.", ex);
            }
        }
    }
}

namespace Application.Handlers.UserAnimals
{
    public class DisassociateUserFromAnimalHandler : IRequestHandler<DisassociateUserFromAnimalCommand, bool>
    {
        private readonly IUserAnimalRepository _userAnimalRepository;

        public DisassociateUserFromAnimalHandler(IUserAnimalRepository userAnimalRepository)
        {
            _userAnimalRepository = userAnimalRepository;
        }

        public async Task<bool> Handle(DisassociateUserFromAnimalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ownership = new Ownership
                {
                    UserId = request.UserAnimal.UserId,
                    AnimalId = request.UserAnimal.AnimalId
                };

                return await _userAnimalRepository.DisassociateUserFromAnimal(ownership);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error disassociating user from animal.", ex);
            }
        }
    }
}
