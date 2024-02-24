using System.Collections;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.Services.RepositoryServices;

public class UnitOfWork(IHttpClientFactory httpClient, IConfiguration configuration, IOptionsSnapshot<AuthenticatedUser> user): IUnitOfWork
{
    private Hashtable? _services;
    private bool _disposed;

    public IGenericService<T> Service<T>() where T : Model
    {
        _services ??= new Hashtable();

        var type = typeof(T).Name;

        if(!_services.ContainsKey(type))
        {
            var repositoryType = typeof(GenericService<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), httpClient, configuration, user);

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
        if(_disposed)
        {
            if(disposing)
            {
                //dispose managed resources

            }
        }
        //dispose unmanaged resources
        _disposed = true;
    }
}
