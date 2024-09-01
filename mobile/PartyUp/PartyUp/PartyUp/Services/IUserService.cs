using System.Threading.Tasks;
using PartyUp.Models;

namespace PartyUp.Services
{
    public interface IUserService
    {
        Task<User> LoginAsync(string email, string password);
        Task<User> SignUpAsync(User newUser);
        Task<bool> ResetPasswordAsync(string email);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> UpdateUserAsync(string userId, User updatedUser);
        Task<bool> DeleteUserAsync(string userId);
    }
}