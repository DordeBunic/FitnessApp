using System.Diagnostics;

namespace FitnessApp.Services.Helpers.Http;

public class CustomHttpHandler : DelegatingHandler
{
    public CustomHttpHandler() : base (new HttpClientHandler())
    {
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
                                                                 CancellationToken cancellationToken)
    {
        foreach (var header in DefaultHeaders.GetHeaders())
        {
            if (request.Headers.Any(x => x.Key == header.Key))
                continue;
            request.Headers.Add(header.Key, header.Value);
        }

        Debug.WriteLine(request.ToCurlRequest());

        return await base.SendAsync(request, cancellationToken);
    }
}
