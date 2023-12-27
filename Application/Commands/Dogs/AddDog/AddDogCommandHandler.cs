using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Application.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
{
    private readonly CleanApiMainContext _dbContext;

    public AddDogCommandHandler(CleanApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newDog = new Dog
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name,
                Weight = request.NewDog.Weight,
                Breed = request.NewDog.Breed
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
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating dog: {ex.Message}");

            // Throw a custom exception for better error handling
            throw new DogCreationException("Error creating dog.", ex);
        }
    }
}

