using System.Net;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Models.Dtos;
using Order.Host.Services.Interfaces;
using Order.Models.Responses;

namespace Order.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderBffController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderBffController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemsResponse<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrdersByUserIdAsync()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var result = await _orderService.GetOrdersByUserIdAsync(userId!);
            return Ok(new ItemsResponse<OrderDto> { Items = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var result = await _orderService.CreateOrderAsync(userId!);
            return Ok(new ItemResponse<int?> { Item = result });
        }
    }
}
