using Domain.Models;
using Infrastructure.Database;
using MediatR;


namespace Application.Commands.Cats.UpdateCats
{
    public class UpdateCatCommandHandler : IRequestHandler<UpdateCatCommand, Cat>
    {
        private readonly MockDatabase _mockDatabase;

        public UpdateCatCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Cat> Handle(UpdateCatCommand request, CancellationToken cancellationToken)
        {
            var catToUpdate = _mockDatabase.Cats.FirstOrDefault(c => c.Id == request.Id);

            if (catToUpdate != null)
            {
                catToUpdate.Name = request.UpdatedCat.Name;
                catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
            }

            return Task.FromResult(catToUpdate);
        }
    }
}