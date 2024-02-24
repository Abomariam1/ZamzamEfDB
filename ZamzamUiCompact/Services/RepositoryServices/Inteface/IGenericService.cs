using ZamzamUiCompact.Models.InterFaces;

namespace ZamzamUiCompact.Services.RepositoryServices.Inteface;

public interface IGenericService<T> where T : IModel
{
    Task<Result<T>> AddAsync(string uri, T model);
    Task<Result<T>> UpdateAsync(string uri, T model);
    Task<Result<int>> DeleteAsync(string uri);
    Task<Result<List<T>>> GetAllAsync(string uri);
    Task<Result<T>> GetByIdAsync(string uri);
    Task<Result<T>> SendRequst(string uri, object obj);
}