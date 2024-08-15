using FitnessApp.Settings;

namespace FitnessApp.Services.Helpers.Http;

public class CustomHttpClient : HttpClient
{
    public CustomHttpClient(HttpMessageHandler handler) : base (handler)
    {
        BaseAddress = new Uri(AppSettings.BaseUrl);
        Timeout = TimeSpan.FromSeconds(10);
    }
}
