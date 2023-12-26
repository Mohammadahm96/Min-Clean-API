using Infrastructure.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly CleanApiMainContext _dbContext;

        public GetDogByIdQueryHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            Dog wantedDog = await _dbContext.Dogs.FirstOrDefaultAsync(dog => dog.Id == request.Id);
            return wantedDog;
        }
    }
}
