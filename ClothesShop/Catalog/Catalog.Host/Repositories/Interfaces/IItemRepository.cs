using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAsync(string? brandFilter, string? categoryFilter);

        Task<int?> Add(string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock);

        Task<int?> Delete(int id);

        Task<int?> Update(int id, string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock);

        Task<Item> GetItemById(int id);

        Task<IEnumerable<string>> GetCategoriesAsync();

        Task<IEnumerable<string>> GetBrandsAsync();
    }
}
