using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _basketService.GetItems();

            if (items is null)
            {
                return RedirectToAction("Index", "Catalog");
            }

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(Item item)
        {
            var basketItem = new ItemBasket
            {
                Id = item.Id,
                Name = item.Name,
                Amount = 1,
                PictureUrl = item.PictureUrl,
                Brand = item.Brand,
                Price = item.Price,
                Size = item.Size
            };
            await _basketService.AddItem(basketItem);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddItemOneMore(int id)
        {
            var basketItems = await _basketService.GetItems();

            if (basketItems == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var item = basketItems.First(b => b.Id == id);
            item.Amount = 1;
            await _basketService.AddItem(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id, int amount)
        {
            await _basketService.DeleteItems(id, amount);
            return RedirectToAction(nameof(Index));
        }
    }
}
