using Application.Commands.Birds.DeleteBirds;
using Infrastructure.Database;
using MediatR;

public class DeleteBirdCommandHandler : IRequestHandler<DeleteBirdCommand, DeleteBirdResult>
{
    private readonly CleanApiMainContext _dbContext;

    public DeleteBirdCommandHandler(CleanApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DeleteBirdResult> Handle(DeleteBirdCommand request, CancellationToken cancellationToken)
    {
        var birdToDelete = await _dbContext.Birds.FindAsync(request.BirdId);

        if (birdToDelete != null)
        {
            _dbContext.Birds.Remove(birdToDelete);
            await _dbContext.SaveChangesAsync();

            // Handle the association with the user (e.g., remove UserAnimal entry)
            await RemoveBirdUserAssociation(request.UserId, birdToDelete.Id);

            return new DeleteBirdResult { IsSuccess = true };
        }

        return new DeleteBirdResult { IsSuccess = false };
    }

    private async Task RemoveBirdUserAssociation(Guid userId, Guid birdId)
    {
        // Handle the association removal logic if needed
        // This could involve removing UserAnimal entry or any other association logic
    }
}



