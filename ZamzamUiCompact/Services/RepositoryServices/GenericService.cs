using ZamzamUiCompact.Execptions;
using ZamzamUiCompact.Models.InterFaces;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.Services.RepositoryServices;

public class GenericService<T>: IGenericService<T> where T : IModel
{

    private readonly HttpClient _httpClient;
    private readonly IOptionsSnapshot<AuthenticatedUser> _user;
    private readonly JsonSerializerOptions option = new()
    {
        PropertyNameCaseInsensitive = true,
    };
    public GenericService(
        IHttpClientFactory httpClient, IConfiguration configuration,
        IOptionsSnapshot<AuthenticatedUser> user)
    {
        _httpClient = httpClient.CreateClient("services");
        _user = user;
        var str = _httpClient.DefaultRequestHeaders
            .FirstOrDefault(x => x.Key == "Authorization")
            .Value;

        if(str is null)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", _user.Value.Token);
        }
    }

    public async Task<Result<T>> AddAsync(string uri, T model)
    {
        if(_user is null)
            throw new UserNullExecption();
        HttpResponseMessage? request = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/{uri}", model, option);
        return await GenericService<T>.SendRequst(request);
    }
    public async Task<Result<T>> UpdateAsync(string uri, T model)
    {
        if(_user is null)
            throw new UserNullExecption();
        HttpResponseMessage? request = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}/{uri}", model, option);
        return await GenericService<T>.SendRequst(request);
    }
    public async Task<Result<int>> DeleteAsync(string uri)
    {
        if(_user is null)
            throw new UserNullExecption();
        HttpResponseMessage? request = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{uri}");
        if(!request.IsSuccessStatusCode)
            return new Result<int> { Succeeded = false, Message = $"fail to process" };

        Result<int>? result = await request.Content.ReadFromJsonAsync<Result<int>>();
        return result ?? new Result<int> { Succeeded = false, Message = $"fail to process" };
    }
    public async Task<Result<List<T>>> GetAllAsync(string uri)
    {
        if(_user is null)
            throw new UserNullExecption();
        Result<List<T>>? request = await _httpClient.GetFromJsonAsync<Result<List<T>>>($"{_httpClient.BaseAddress}/{uri}");
        return request ?? new Result<List<T>>() { Succeeded = false, Message = "faild to get the List" };
    }
    public async Task<Result<T>> GetByIdAsync(string uri)
    {
        if(_user is null)
            throw new UserNullExecption();
        Result<T>? request = await _httpClient.GetFromJsonAsync<Result<T>>($"{_httpClient.BaseAddress}/{uri}");
        return request ?? new Result<T>() { Succeeded = false, Message = "faild to get the List" };
    }
    public async Task<Result<T>> SendRequst(string uri, object obj)
    {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/{uri}", obj, option);
        var result = await SendRequst(response);
        return result;
    }
    private static async Task<Result<T>> SendRequst(HttpResponseMessage responseMessage)
    {
        if(!responseMessage.IsSuccessStatusCode)
        {
            return new Result<T> { Succeeded = false, Message = $"fail to process" };
        }
        Result<T>? result = await responseMessage.Content.ReadFromJsonAsync<Result<T>>();
        return result ?? new Result<T> { Succeeded = false, Message = $"fail to process" };
    }
}
