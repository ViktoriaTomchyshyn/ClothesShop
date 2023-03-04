using Infrastructure.Services.Interfaces;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(string? brand, string? category)
    {
        var filters = new Dictionary<CatalogTypeFilter, string>();

        if (brand != null && brand != "all")
        {
            filters.Add(CatalogTypeFilter.Brand, brand);
        }

        if (category != null && category != "all")
        {
            filters.Add(CatalogTypeFilter.Category, category);
        }

        var result = await _httpClient.SendAsync<Catalog, ItemsRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/items",
           HttpMethod.Post, 
           new ItemsRequest<CatalogTypeFilter>()
           {
               Filters = filters
           });

        _logger.LogInformation($"Received {result.Data.Count} items from catalog");

        return result!;
    }

    public async Task<Item> GetCatalogItem(int id)
    {

        var result = await _httpClient.SendAsync<ItemResponse<Item>, IdRequest>($"{_settings.Value.CatalogUrl}/item",
           HttpMethod.Post,
           new IdRequest() { Id = id });
        _logger.LogInformation(result.Item.Name);
        return result.Item;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        var list = new List<SelectListItem>();

        var result = await _httpClient.SendAsync<IEnumerable<string>, object>(
            $"{_settings.Value.CatalogUrl}/brands",
            HttpMethod.Post,
            new { });

        _logger.LogInformation($"Received {result.Count()} brands");

        foreach (var brand in result)
        {
            list.Add(new SelectListItem() { Value = brand, Text = brand });
        }

        return list;
    }

    public async Task<IEnumerable<SelectListItem>> GetCategories()
    {
        var list = new List<SelectListItem>();

        var result = await _httpClient.SendAsync<IEnumerable<string>, object>(
            $"{_settings.Value.CatalogUrl}/categories",
            HttpMethod.Post,
            new { });

        _logger.LogInformation($"Received {result.Count()} categories");

        foreach (var category in result)
        {
            list.Add(new SelectListItem() { Value = category, Text = category });
        }

        return list;
    }
}
