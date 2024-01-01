using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System;

public interface IUserRepository
{
    Task<UserModel> GetUserById(Guid userId);
    Task<UserModel> GetUserByUsername(string username);
    Task AddUser(UserModel user);
    Task UpdateUserAsync(UserModel user);
    Task DeleteUserAsync(UserModel user);
    Task SaveChangesAsync();
    
}
