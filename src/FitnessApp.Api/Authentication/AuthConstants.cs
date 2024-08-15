namespace FitenssApp.Api.Authentication;

public static class AuthConstants
{
    public const string ApiKeySectionName = "Authentication:ApiKey";
    public const string ApiKeyHeaderName = "x-apiKey";
    public const string AppIdHeaderName = "x-appId";
    public const string AppIdHeaderValue = "FitnessApp.Maui";
    public const string AppVersionHeaderName = "x-appVersion";
    public static string[] AppVersionHeaderValues = { "1.0" };
}
