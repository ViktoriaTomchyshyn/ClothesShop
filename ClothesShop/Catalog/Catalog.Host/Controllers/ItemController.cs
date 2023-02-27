using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("catalog.catalogitem")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService _itemService;

        public ItemController(
            ILogger<ItemController> logger,
            IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(CreateItemRequest request)
        {
            var result = await _itemService.Add(request.Name, request.Description, request.Category, request.Brand, request.Size, request.Price, request.PictureFileName, request.AvailableStock);
            return Ok(new AddItemResponse<int?>() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeleteItemResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _itemService.Delete(id);
            return Ok(new DeleteItemResponse<int?>() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UpdateItemResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateItemRequest request)
        {
            var result = await _itemService.Update(request.Id, request.Name, request.Description, request.Category, request.Brand, request.Size, request.Price, request.PictureFileName, request.AvailableStock);
            return Ok(new UpdateItemResponse<int?>() { Id = result });
        }
    }
}
