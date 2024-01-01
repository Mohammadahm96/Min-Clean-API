using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

public class CatRepository : ICatRepository
{
    private readonly CleanApiMainContext _dbContext;

    public CatRepository(CleanApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Cat> GetCatById(Guid catId)
    {
        return await _dbContext.Cats.FirstOrDefaultAsync(cat => cat.Id == catId);
    }

    public async Task<List<Cat>> GetAllCats()
    {
        return await _dbContext.Cats.ToListAsync();
    }

    public async Task AddCat(Cat cat, Guid userId)
    {
        _dbContext.Cats.Add(cat);

        var ownership = new Ownership
        {
            UserId = userId,
            AnimalId = cat.Id
        };

        _dbContext.Ownerships.Add(ownership);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCat(Cat cat)
    {
        _dbContext.Cats.Update(cat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCat(Cat cat)
    {
        _dbContext.Cats.Remove(cat);
        await _dbContext.SaveChangesAsync();
    }
}

