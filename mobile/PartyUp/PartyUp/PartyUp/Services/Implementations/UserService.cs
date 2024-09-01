using System.Threading.Tasks;
using PartyUp.Models;

namespace PartyUp.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            return await _httpService.PostAsync<User>("api/user/login", loginData, isAuthorized:false);
        }

        public async Task<User> SignUpAsync(User newUser)
        {
            return await _httpService.PostAsync<User>("api/user/register", newUser, isAuthorized:false);
        }

        public async Task<bool> ResetPasswordAsync(string email)
        {
            var resetData = new { Email = email };
            return await _httpService.PostAsync<bool>("api/user/reset-password", resetData, isAuthorized: false);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _httpService.GetAsync<User>($"api/user/{userId}");
        }

        public async Task<User> UpdateUserAsync(string userId, User updatedUser)
        {
            return await _httpService.PutAsync<User>($"users/{userId}", updatedUser);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            return await _httpService.DeleteAsync($"api/user/{userId}");
        }
    }
}