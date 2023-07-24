namespace Zamzam.Core.Services
{
    public interface IItemService
    {
        Task<Item> GetItemAsync(int id);

    }
}
