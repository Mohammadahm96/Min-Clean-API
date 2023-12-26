using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var catToUpdate = await _dbContext.Cats.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (catToUpdate != null)
            {
                catToUpdate.Name = request.UpdatedCat.Name;
                catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }

            return catToUpdate;
        }
    }
}
