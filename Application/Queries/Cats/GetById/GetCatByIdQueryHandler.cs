using Infrastructure.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Cats.GetCatById
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat>
    {
        private readonly CleanApiMainContext _dbContext;

        public GetCatByIdQueryHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cat> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            Cat wantedCat = await _dbContext.Cats.FirstOrDefaultAsync(cat => cat.Id == request.Id);
            return wantedCat;
        }
    }
}
