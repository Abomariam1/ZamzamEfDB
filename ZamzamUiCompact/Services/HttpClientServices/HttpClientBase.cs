using ZamzamUiCompact.Services.HttpClientServices.Inteface;

namespace ZamzamUiCompact.Services.HttpClientServices;

public class HttpClientBase : IHttpClientBase
{
    private readonly HttpClient _httpClient;
    private readonly IAuthenticatedUser _user;

    public HttpClientBase(HttpClient httpClient, IAuthenticatedUser user)
    {
        _httpClient = httpClient;
        _user = user.GetUser();
        _httpClient.DefaultRequestHeaders.Add("Authorization", _user.Token);
    }

    public async Task<HttpResponseMessage> Add(Uri uri, ObservableObject observableObject)
    {
        if (_user is null)
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        return await _httpClient.PostAsJsonAsync(uri, observableObject);
    }

    public Task<HttpResponseMessage> Delete(Uri uri, ObservableObject observableObject)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> GetAll(Uri uri, ObservableObject observableObject)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> Update(Uri uri, ObservableObject observableObject)
    {
        throw new NotImplementedException();
    }
}
