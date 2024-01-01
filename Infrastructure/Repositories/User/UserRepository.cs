using System;
using System.Threading.Tasks;
using Domain.Models.User;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
public class UserRepository : IUserRepository
{
    private readonly CleanApiMainContext _dbContext;

    public UserRepository(CleanApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserModel> GetUserById(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<UserModel> GetUserByUsername(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> UserExists(string username)
    {
        return await _dbContext.Users.AnyAsync(u => u.Username == username);
    }

    public async Task AddUser(UserModel user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateUserAsync(UserModel user)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteUserAsync(UserModel user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
