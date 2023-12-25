using Infrastructure.Database;
using MediatR;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Birds.GetAllBirds
{
    internal sealed class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly MockDatabase _mockDatabase;

        public GetAllBirdsQueryHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            List<Bird> allBirdsFromMockDatabase = _mockDatabase.Birds;
            return await Task.FromResult(allBirdsFromMockDatabase);
        }
    }
}


