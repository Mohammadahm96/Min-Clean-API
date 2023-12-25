using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Dogs.DeleteDogs;
public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, DeleteDogResult>
{
    private readonly MockDatabase _mockDatabase;

    public DeleteDogCommandHandler(MockDatabase mockDatabase)
    {
        _mockDatabase = mockDatabase;
    }

    public async Task<DeleteDogResult> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
    {
        // Implement logic to delete the dog from the database based on the command
        var dogToDelete = _mockDatabase.Dogs.FirstOrDefault(d => d.Id == request.DogId);

        if (dogToDelete != null)
        {
            _mockDatabase.Dogs.Remove(dogToDelete);
            return new DeleteDogResult { IsSuccess = true };
        }

        return new DeleteDogResult { IsSuccess = false };
    }
}