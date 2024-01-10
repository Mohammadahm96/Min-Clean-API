using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UserAnimalRepository : IUserAnimalRepository
{
    private readonly CleanApiMainContext _dbContext;

    public UserAnimalRepository(CleanApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AssociateUserWithAnimal(Ownership ownership)
    {
        try
        {
            _dbContext.Ownerships.Add(ownership);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception("Error associating user with animal.", ex);
        }
    }

    public async Task<bool> DisassociateUserFromAnimal(Ownership ownership)
    {
        try
        {
            var existingOwnership = await _dbContext.Ownerships
                .FirstOrDefaultAsync(o => o.UserId == ownership.UserId && o.AnimalId == ownership.AnimalId);

            if (existingOwnership != null)
            {
                _dbContext.Ownerships.Remove(existingOwnership);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception("Error disassociating user from animal.", ex);
        }
    }
    public async Task<List<Ownership>> GetAllAssociatedAnimals()
    {
        return await _dbContext.Ownerships
            .Include(o => o.Animal)
            .ToListAsync();
    }

}

