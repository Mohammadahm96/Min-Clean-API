using Infrastructure.Database;
using MediatR;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Birds.GetAllBirds
{
    internal sealed class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly CleanApiMainContext _dbContext;

        public GetAllBirdsQueryHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            List<Bird> allBirdsFromdbContext = await _dbContext.Birds.ToListAsync();
            return allBirdsFromdbContext;
        }
    }
}


