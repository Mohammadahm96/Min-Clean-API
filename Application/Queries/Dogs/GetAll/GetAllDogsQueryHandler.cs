using Infrastructure.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Dogs.GetAll
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly CleanApiMainContext _dbContext;

        public GetAllDogsQueryHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            List<Dog> allDogsFromDatabase = await _dbContext.Dogs.ToListAsync();
            return allDogsFromDatabase;
        }
    }
}
