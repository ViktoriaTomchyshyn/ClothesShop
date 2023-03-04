using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogResponse> GetItemsAsync(Dictionary<ItemTypeFilter, string>? filters);

        Task<ItemDto> GetItemAsync(int id);

        Task<IEnumerable<string>> GetBrandsAsync();

        Task<IEnumerable<string>> GetCategoriesAsync();
    }
}
