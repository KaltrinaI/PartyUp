using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PartyUp.Services;
using Xamarin.Essentials;

public class HttpService : IHttpService
{
    private const string BaseAddress = "http://192.168.1.5:5050";
    private readonly HttpClient _httpClient;
    private string _token;

    public HttpService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseAddress)
        };
    }

    private async Task EnsureTokenAsync()
    {
        if (string.IsNullOrEmpty(_token))
        {
            _token = await SecureStorage.GetAsync("jwt_token");
        }
    }

    private async Task AddAuthorizationHeaderIfRequiredAsync(bool isAuthorized)
    {
        if (isAuthorized)
        {
            await EnsureTokenAsync();
            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            }
        }
        else
        {
            _httpClient.DefaultRequestHeaders.Authorization = null; // Remove the Authorization header if not authorized
        }
    }

    public async Task<T> GetAsync<T>(string uri, bool isAuthorized = true)
    {
        try
        {
            await AddAuthorizationHeaderIfRequiredAsync(isAuthorized);
            var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(content);
        }
        catch (Exception ex)
        {
            throw new Exception($"GetAsync error: {ex.Message}", ex);
        }
    }

    public async Task<T> PostAsync<T>(string uri, object data, bool isAuthorized = true)
    {
        try
        {
            await AddAuthorizationHeaderIfRequiredAsync(isAuthorized);
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }
        catch (Exception ex)
        {
            //throw new Exception($"PostAsync error: {ex.Message}", ex);
            return default;
        }
    }

    public async Task PostAsync(string uri, object data, bool isAuthorized = true)
    {
        try
        {
            await AddAuthorizationHeaderIfRequiredAsync(isAuthorized);
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            
        }
        catch (Exception ex)
        {
            //throw new Exception($"PostAsync error: {ex.Message}", ex);
            return ;
        }
    }

    public async Task<T> PutAsync<T>(string uri, object data, bool isAuthorized = true)
    {
        try
        {
            await AddAuthorizationHeaderIfRequiredAsync(isAuthorized);
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var method = new HttpMethod("PUT");
            var request = new HttpRequestMessage(method, uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }
        catch (Exception ex)
        {
            throw new Exception($"PutAsync error: {ex.Message}", ex);
        }
    }
    

    public async Task<bool> DeleteAsync(string uri, bool isAuthorized = true)
    {
        try
        {
            await AddAuthorizationHeaderIfRequiredAsync(isAuthorized);
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"DeleteAsync error: {ex.Message}", ex);
        }
    }
}
