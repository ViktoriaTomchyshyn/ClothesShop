using Basket.Host.Models.Dtos;
using Basket.Models.Responses;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Models.Requests;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("basket.basket")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketService _basketService;

        public BasketController(
            ILogger<BasketController> logger,
            IBasketService basketService)
        {
            _logger = logger;
            _basketService = basketService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemsResponse<BasketItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItems(ItemRequest<string> request)
        {
            var result = await _basketService.GetItems(request.Item);
            await _basketService.DeleteBasket(request.Item);
            return Ok(new ItemsResponse<BasketItemDto> { Items = (IEnumerable<BasketItemDto>)result });
        }
    }
}
