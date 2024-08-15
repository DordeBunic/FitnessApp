namespace FitenssApp.Api.Authentication;

public class AuthenticationFilter : IEndpointFilter
{
    public readonly IConfiguration _configuration;

    public AuthenticationFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey)
            || !context.HttpContext.Request.Headers.TryGetValue(AuthConstants.AppIdHeaderName, out var appId)
            || !context.HttpContext.Request.Headers.TryGetValue(AuthConstants.AppVersionHeaderName, out var appVersion))
        {
            return TypedResults.Unauthorized();
        }

        var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

        if (string.IsNullOrEmpty(apiKey) || !apiKey.Equals(extractedApiKey) ||
            string.IsNullOrEmpty(appId) || !appId.Equals(AuthConstants.AppIdHeaderValue) ||
            string.IsNullOrEmpty(appVersion) || !AuthConstants.AppVersionHeaderValues.Any(x=> x == appVersion))
        {
            return TypedResults.Unauthorized();
        }
            return await next(context);
    }
}
