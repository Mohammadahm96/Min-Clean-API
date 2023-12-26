using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Queries.Birds.GetAllBirds
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly CleanApiMainContext _dbContext;

        public GetAllBirdsQueryHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            List<Bird> allBirdsFromDb = await _dbContext.Birds.ToListAsync();
            return allBirdsFromDb;
        }
    }
}



