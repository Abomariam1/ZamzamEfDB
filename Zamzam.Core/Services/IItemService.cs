namespace Zamzam.Core.Services
{
    public interface IItemService
    {
        Task<Item> GetItemAsync(int id);
        Task DeleteItemAsync(int id);
        Task<ICollection<Item>> GetItemsAsync();
    }
}
