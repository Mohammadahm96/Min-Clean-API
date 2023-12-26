using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
{
    private readonly CleanApiMainContext _dbContext;

    public AddDogCommandHandler(CleanApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
    {
        var newDog = new Dog
        {
            Id = Guid.NewGuid(),
            Name = request.NewDog.Name
        };

        // Add dog to the database
        _dbContext.Dogs.Add(newDog);

        // Create ownership relationship
        var ownership = new Ownership
        {
            UserId = request.UserId,
            AnimalId = newDog.Id
        };

        // Add ownership to the database
        _dbContext.Ownerships.Add(ownership);

        await _dbContext.SaveChangesAsync();

        return newDog;
    }
}

