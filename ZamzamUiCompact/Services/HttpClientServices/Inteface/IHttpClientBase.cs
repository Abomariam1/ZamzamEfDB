namespace ZamzamUiCompact.Services.HttpClientServices.Inteface;

public interface IHttpClientBase
{
    Task<HttpResponseMessage> Add(Uri uri, ObservableObject observableObject);
    Task<HttpResponseMessage> Update(Uri uri, ObservableObject observableObject);
    Task<HttpResponseMessage> Delete(Uri uri, ObservableObject observableObject);
    Task<HttpResponseMessage> GetAll(Uri uri, ObservableObject observableObject);
}