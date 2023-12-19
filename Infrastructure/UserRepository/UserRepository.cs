// UserRepository.cs in Infrastructure layer
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.User;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Shared.Repositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CleanApiMainContext _dbContext;

        public UserRepository(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> RegisterUser(UserModel user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserModel?> GetUserByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
