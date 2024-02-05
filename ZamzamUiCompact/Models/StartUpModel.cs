namespace ZamzamUiCompact.Models;

public class StartUpModel
{
    private readonly HttpClient _httpClient;

    public StartUpModel(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient();
    }
}
