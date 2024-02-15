using System.Collections;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.Services.RepositoryServices;

public class UnitOfWork(HttpClient httpClient, IAuthenticatedUser user) : IUnitOfWork
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IAuthenticatedUser _user = user;
    private Hashtable? _services;
    private bool _disposed;

    public IGenericService<T> Service<T>() where T : Model
    {
        _services ??= new Hashtable();

        var type = typeof(T).Name;

        if (!_services.ContainsKey(type))
        {
            var repositoryType = typeof(GenericService<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _httpClient, _user);

            _services.Add(type, repositoryInstance);
        }
        return (GenericService<T>)_services[type]!;
    }
    public void Dispose()
    {
        Dispose(true);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            if (disposing)
            {
                //dispose managed resources

            }
        }
        //dispose unmanaged resources
        _disposed = true;
    }
}
