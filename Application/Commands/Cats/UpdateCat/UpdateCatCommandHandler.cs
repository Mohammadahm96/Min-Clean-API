using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Cats.UpdateCats
{
    public class UpdateCatCommandHandler : IRequestHandler<UpdateCatCommand, Cat>
    {
        private readonly CleanApiMainContext _dbContext;

        public UpdateCatCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cat> Handle(UpdateCatCommand request, CancellationToken cancellationToken)
        {
            var catToUpdate = _dbContext.Cats.FirstOrDefault(c => c.Id == request.Id);

            if (catToUpdate != null)
            {
                catToUpdate.Name = request.UpdatedCat.Name;
                catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
                await _dbContext.SaveChangesAsync();
            }

            return catToUpdate;
        }
    }
}


