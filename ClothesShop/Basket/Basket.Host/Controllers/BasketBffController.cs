using Basket.Host.Models;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddItem(AddItemRequest<Item> request)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        await _basketService.AddItem(basketId!, request.Item);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemsResponse<Item>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItems()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var items = await _basketService.GetItems(basketId!);
        return Ok(new GetItemsResponse<Item> { Items = items });
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteItem(DeleteItemRequest request)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        await _basketService.DeleteItem(basketId!, request.ItemId, request.Amount);
        return Ok();
    }
}