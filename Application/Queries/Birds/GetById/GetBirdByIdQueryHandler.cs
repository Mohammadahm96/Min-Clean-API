using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Birds.GetBirdById
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird>
    {
        private readonly MockDatabase _mockDatabase;

        public GetBirdByIdQueryHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Bird> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            Bird WantedBird = _mockDatabase.Birds.FirstOrDefault(bird => bird.Id == request.Id)!;
            return Task.FromResult(WantedBird);
        }
    }
}

