namespace Zamzam.Core.Services
{
    public interface IItemService : IDataService<Item>
    {
        Task<Item> FindItemAsync(string name);

    }
}
