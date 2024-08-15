
namespace FitnessApp.Services.Interfaces.Core
{
    public interface IApiService
    {
        Task<string> GetRequest(string url, 
                                Dictionary<string, string>? 
                                headers = null);
        Task<TResult> GetRequest<TResult>(string url, 
                                          Dictionary<string, string>? headers = null);


        Task<string> PostRequest(string url, 
                                 object? data = null, 
                                 Dictionary<string, string>? headers = null);
        Task<TResult> PostRequest<TResult>(string url, 
                                           object? data = null, 
                                           Dictionary<string, string>? headers = null);


        Task<bool> DeleteRequest(string url, 
                                 Dictionary<string, string>? headers = null);

        
        Task<string> UpdateRequest(string url, 
                                   object? data, 
                                   Dictionary<string, string>? headers = null);
        Task<TResult> UpdateRequest<TResult>(string url, 
                                             object? data, 
                                            Dictionary<string, string>? headers = null);
    }
}
