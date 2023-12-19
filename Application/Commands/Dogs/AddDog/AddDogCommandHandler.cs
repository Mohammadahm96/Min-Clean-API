using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Dogs
{
    internal sealed class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly CleanApiMainContext _dbContext;

        public AddDogCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            Dog dogToCreate = new Dog
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };

            _dbContext.Dogs.Add(dogToCreate);
            await _dbContext.SaveChangesAsync();

            return dogToCreate;
        }
    }
}
