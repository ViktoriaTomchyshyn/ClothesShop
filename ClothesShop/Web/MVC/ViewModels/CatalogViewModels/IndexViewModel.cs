using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.CatalogViewModels;

public class IndexViewModel
{
    public IEnumerable<Item> CatalogItems { get; set; }
    public IEnumerable<SelectListItem> Brands { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
    public int? BrandFilterApplied { get; set; }
    public int? CategoriesFilterApplied { get; set; }
}
