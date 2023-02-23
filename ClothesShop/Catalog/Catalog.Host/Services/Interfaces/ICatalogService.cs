using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ItemDto>> GetAll();
    }
}
