using System.Threading.Tasks;
using Domain.Models.User;
using MediatR;

namespace Shared.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> RegisterUser(UserModel user);
        Task<UserModel> GetUserByUsername(string username);
    }
}

