namespace ZamzamUiCompact.Helpers;

public class ApiHelper(IHttpClientFactory httpClient) : IApiHelper, IDisposable
{
    private readonly HttpClient _httpClient = httpClient.CreateClient();

    public async Task<T> GetModelAsync<T>(string uri, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<T>(uri, cancellationToken);
        return response;
    }

    public async Task<HttpResponseMessage> PostModelAsync<T>(string uri, T value)
    {
        HttpResponseMessage? respons = await _httpClient.PostAsJsonAsync<T>($"{_httpClient.BaseAddress}/{uri}", value);
        return respons;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

}
