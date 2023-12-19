using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Dogs
{
    internal sealed class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly CleanApiMainContext _dbContext;

        public GetAllDogsQueryHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            List<Dog> allDogsFromdbContext = await _dbContext.Dogs.ToListAsync();
            return allDogsFromdbContext;
        }
    }
}

