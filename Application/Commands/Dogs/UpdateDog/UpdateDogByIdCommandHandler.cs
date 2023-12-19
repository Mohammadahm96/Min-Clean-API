using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Dogs.UpdateDog
{
    internal class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly CleanApiMainContext _dbContext;

        public UpdateDogByIdCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            var dogToUpdate = _dbContext.Dogs.FirstOrDefault(dog => dog.Id == request.Id);

            if (dogToUpdate != null)
            {
                dogToUpdate.Name = request.UpdatedDog.Name;
                await _dbContext.SaveChangesAsync();
            }

            return dogToUpdate;
        }
    }
}

