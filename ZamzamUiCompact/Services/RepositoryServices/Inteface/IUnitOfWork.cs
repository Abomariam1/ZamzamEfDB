namespace ZamzamUiCompact.Services.RepositoryServices.Inteface;

public interface IUnitOfWork
{
    IGenericService<T> Service<T>() where T : Model;
}
