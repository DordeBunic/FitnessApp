namespace FitnessApp.Services.Helpers.Http;
public static class HttpExtensions
{
    public  static string ToCurlRequest(this HttpRequestMessage request)
    {
        var content = request.Content == null ? null : request.Content.ReadAsStringAsync().Result;
        var headers = request.Headers.ToDictionary(k => k.Key, v => v.Value.FirstOrDefault());
        return ToCurlRequest(request!.RequestUri!.ToString(), headers, content);
    }
    public static string ToCurlRequest(this string url, 
                                       Dictionary<string, string> headers, 
                                       string? content)
    {
        var headerString = string.Join($"\\\n", headers.Keys.Select(k => $"  -H '{k}: {headers[k]}' "));

        string curl = $"curl '{url}' \\\n{headerString}";

        if (!string.IsNullOrEmpty(content))
        {
            curl = curl + $"\\\n  -d '{content.Replace("'", "\\'")}'";
        }

        return curl;
    }
}
