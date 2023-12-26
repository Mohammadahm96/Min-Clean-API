using Infrastructure.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Birds.GetBirdById
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird>
    {
        private readonly CleanApiMainContext _dbContext;

        public GetBirdByIdQueryHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Bird> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            Bird wantedBird = await _dbContext.Birds.FirstOrDefaultAsync(bird => bird.Id == request.Id);
            return wantedBird;
        }
    }
}
