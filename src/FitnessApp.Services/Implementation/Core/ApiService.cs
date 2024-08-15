using CommunityToolkit.Maui.Core;
using FitnessApp.Services.Helpers.ErrorHandling;
using FitnessApp.Services.Helpers.Http;
using FitnessApp.Services.Interfaces.Core;
using System.Net.Http.Json;
using System.Text.Json;

namespace FitnessApp.Services.Implementation.Core;
public class ApiService : IApiService
{
    private readonly CustomHttpClient _httpClient;
    private readonly IToast _toast;


    private readonly ErrorHandlingService _errorHandlingService;
    public ApiService(CustomHttpClient httpClient,
        IToast toast)
    {
        _httpClient = httpClient;
        _toast = toast;

        _errorHandlingService = new ErrorHandlingService();
    }
    #region GetRequests
    private async Task<string> GetRequestApi(string url,
                                             Dictionary<string, string>? headers = null)
    {
        if (headers != null)
            AddHeadersToClient(headers);
        var result = await _httpClient.GetAsync(url);
        if (headers != null)
            RemoveHeadersToClient(headers);

        if (result.IsSuccessStatusCode)
            return await result.Content.ReadAsStringAsync();
        return string.Empty;
    }


    public async Task<string> GetRequest(string url,
                                         Dictionary<string, string>? headers = null)
    {
        return await _errorHandlingService.HandleTask(GetRequestApi(url, headers));
    }
    public async Task<TResult> GetRequest<TResult>(string url, 
                                                   Dictionary<string, string>? headers = null)
    {
        var result = await GetRequest(url, headers);
        if (!string.IsNullOrEmpty(result))
            return JsonSerializer.Deserialize<TResult>(result)!;
        return default!;
    }
    #endregion

    #region PostRequest
    private async Task<string> PostRequestApi(string url, 
                                              object? data = null, 
                                              Dictionary<string, string>? headers = null)
    {
        if (headers != null)
            AddHeadersToClient(headers);
        var result = await _httpClient.PostAsync(url, JsonContent.Create(data));
        if (headers != null)
            RemoveHeadersToClient(headers);
        if (result.IsSuccessStatusCode)
            return await result.Content.ReadAsStringAsync();
        return string.Empty;
    }

    public async Task<string> PostRequest(string url, 
                                          object? data = null, 
                                          Dictionary<string, string>? headers = null)
    {
        return await _errorHandlingService.HandleTask(PostRequestApi(url, data, headers));
    }
    public async Task<TResult> PostRequest<TResult>(string url, 
                                                    object? data = null, 
                                                    Dictionary<string, string>? headers = null)
    {
        var result = await PostRequest(url, data, headers);
        if (!string.IsNullOrEmpty(result))
            return JsonSerializer.Deserialize<TResult>(result)!;
        return default!;
    }
    #endregion

    #region DeleteRequest
    private async Task<bool> DeleteRequestApi(string url, 
                                              Dictionary<string, string>? headers = null)
    {
        if (headers != null)
            AddHeadersToClient(headers);
        var result = await _httpClient.DeleteAsync(url);
        if (headers != null)
            RemoveHeadersToClient(headers);
        if (result.IsSuccessStatusCode)
            return true;
        return false;
    }
    public async Task<bool> DeleteRequest(string url, 
                                          Dictionary<string, string>? headers = null)
    {
        return await _errorHandlingService.HandleTask(DeleteRequestApi(url, headers));
    }
    #endregion

    #region UpdateRequest
    private async Task<string> UpdateRequestApi(string url, 
                                                object? data = null, 
                                                Dictionary<string, string>? headers = null)
    {
        if (headers != null)
            AddHeadersToClient(headers);
        var result = await _httpClient.PatchAsync(url, JsonContent.Create(data));
        if (headers != null)
            RemoveHeadersToClient(headers);
        if (result.IsSuccessStatusCode)
            return await result.Content.ReadAsStringAsync();
        return string.Empty;
    }
    public async Task<string> UpdateRequest(string url, 
                                            object? data = null, 
                                            Dictionary<string, string>? headers = null)
    {
        return await _errorHandlingService.HandleTask(UpdateRequestApi(url, data, headers));
    }
    public async Task<TResult> UpdateRequest<TResult>(string url, 
                                                      object? data = null, 
                                                      Dictionary<string, string>? headers = null)
    {
        var result = await UpdateRequest(url, data, headers);
        if (!string.IsNullOrEmpty(result))
            return JsonSerializer.Deserialize<TResult>(result)!;
        return default!;
    }
    #endregion

    #region Helpers
    private void AddHeadersToClient(Dictionary<string, string> headers)
    {
        foreach (var item in headers)
        {
            if (_httpClient.DefaultRequestHeaders.Any(x => x.Key == item.Key))
                continue;
            _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
        }
    }

    private void RemoveHeadersToClient(Dictionary<string, string> headers)
    {
        foreach (var item in headers)
        {
            if (_httpClient.DefaultRequestHeaders.Any(x => x.Key == item.Key))
                continue;
            _httpClient.DefaultRequestHeaders.Remove(item.Key);
        }
    }
    #endregion

}
