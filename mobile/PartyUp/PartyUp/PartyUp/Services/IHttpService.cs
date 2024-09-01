using System.Threading.Tasks;

namespace PartyUp.Services
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri, bool isAuthorized=true);
        Task<T> PostAsync<T>(string uri, object data, bool isAuthorized=true);
        Task PostAsync(string uri, object data, bool isAuthorized=true);

        Task<T> PutAsync<T>(string uri, object data, bool isAuthorized = true);
        Task<bool> DeleteAsync(string uri, bool isAuthorized = true);
    }
}