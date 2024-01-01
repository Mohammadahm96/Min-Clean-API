using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DogRepository : IDogRepository
{
    private readonly CleanApiMainContext _dbContext;
    private readonly ILogger<DogRepository> _logger;

    public DogRepository(CleanApiMainContext cleanApiMainContext, ILogger<DogRepository> logger)
    {
        _dbContext = cleanApiMainContext;
        _logger = logger;
    }

    public async Task<List<Dog>> GetAllDogs()
    {
        try
        {
            List<Dog> allDogsFromDatabase = await _dbContext.Dogs.ToListAsync();
            return allDogsFromDatabase;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while getting all dogs from the database", ex);
            throw new Exception("An error occurred while getting all dogs from the database", ex);
        }
    }

    public async Task<Dog?> GetDogById(Guid dogId)
    {
        try
        {
            Dog? wantedDog = await _dbContext.Dogs.FindAsync(dogId);
            return wantedDog;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting a dog by Id {dogId} from the database", ex);
            throw new Exception($"An error occurred while getting a dog by Id {dogId} from the database", ex);
        }
    }

    public async Task AddDog(Dog dog, Guid userId)
    {
        try
        {
            // Add dog to the database
            _dbContext.Dogs.Add(dog);

            // Create ownership relationship
            var ownership = new Ownership
            {
                UserId = userId,
                AnimalId = dog.Id
            };

            // Add ownership to the database
            _dbContext.Ownerships.Add(ownership);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while adding a dog to the database", ex);
            throw new Exception($"An error occurred while adding a dog to the database", ex);
        }
    }
    public async Task UpdateDog(Dog dog)
    {
        try
        {
            // Update dog in the database
            _dbContext.Dogs.Update(dog);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating a dog in the database", ex);
            throw new Exception($"An error occurred while updating a dog in the database", ex);
        }
    }
    public async Task DeleteDog(Dog dog)
    {
        try
        {
            // Delete dog from the database
            _dbContext.Dogs.Remove(dog);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting a dog from the database", ex);
            throw new Exception($"An error occurred while deleting a dog from the database", ex);
        }
    }
}
