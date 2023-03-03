using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Scope("catalog.catalogitem")]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogBffController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly ICatalogService _catalogService;

        public CatalogBffController(
            ILogger<CatalogBffController> logger,
            ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CatalogResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items()
        {
            var result = await _catalogService.GetItemsAsync();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Item(GetByIdRequest request)
        {
            var result = await _catalogService.GetItemAsync(request.Id);
            return Ok(new ItemResponse<ItemDto> { Item = result } );
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Brands()
        {
            var result = await _catalogService.GetBrandsAsync();
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Categories()
        {
            var result = await _catalogService.GetCategoriesAsync();
            return Ok(result);
        }
    }
}
