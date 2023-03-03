using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogResponse> GetItemsAsync();

        Task<ItemDto> GetItemAsync(int id);

        Task<IEnumerable<string>> GetBrandsAsync();

        Task<IEnumerable<string>> GetCategoriesAsync();
    }
}
