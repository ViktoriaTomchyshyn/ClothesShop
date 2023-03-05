using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;

namespace MVC.Controllers;

public class CatalogController : Controller
{
    private  readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index(string? brandFilterApplied, string? categoriesFilterApplied)
    {   
        var catalog = await _catalogService.GetCatalogItems(brandFilterApplied, categoriesFilterApplied);
        if (catalog == null)
        {
            return View("Error");
        }
      
        var vm = new IndexViewModel()
        {
            CatalogItems = catalog.Data,
            Brands = await _catalogService.GetBrands(),
            Categories = await _catalogService.GetCategories(),
        };

        return View(vm);
    }

    [Authorize]
    public async Task<IActionResult> ItemInfo(int id)
    {
        var item = await _catalogService.GetCatalogItem(id);
        if (item is null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(item);
    }
}