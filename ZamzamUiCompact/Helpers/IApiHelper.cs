namespace ZamzamUiCompact.Helpers;

public interface IApiHelper
{
    Task<T> GetModelAsync<T>(string uri, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostModelAsync<T>(string uri, T value);
}