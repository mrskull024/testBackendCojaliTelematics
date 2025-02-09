using Core.DTOs;
using Core.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<int> AddUser(UserAddDTO user);
        Task<bool> GetUserByEmail(string email);
    }
}
