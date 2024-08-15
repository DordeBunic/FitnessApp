using FitnessApp.Models.Domain.User;

namespace FitnessApp.Services.Helpers.Http;

public static class DefaultHeaders
{
    private static Dictionary<string, string> _headers;
    public static Dictionary<string,string> GetHeaders()
    {
        return _headers;
    }
    public static void UpdateHeaders(User? user = null)
    {
        if (user != null && !string.IsNullOrEmpty(user.Id))
            _headers = new Dictionary<string, string>
            {
                {"x-userId", user.Id! },
                {"x-appId", AppInfo.Current.Name },
                {"x-appVersion", AppInfo.Current.VersionString },
                {"x-apiKey", "80b7eca6a77d44f9a918bd84bc7e07bc" }
            };
        else
        {
            _headers = new Dictionary<string, string>
            {
                {"x-appId", AppInfo.Current.Name },
                {"x-appVersion", AppInfo.Current.VersionString },
                {"x-apiKey", "80b7eca6a77d44f9a918bd84bc7e07bc" }
            };
        }
    }
    
}