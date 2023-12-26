using Infrastructure.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly CleanApiMainContext _dbContext;

        public GetAllCatsQueryHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            List<Cat> allCatsFromDatabase = await _dbContext.Cats.ToListAsync();
            return allCatsFromDatabase;
        }
    }
}

