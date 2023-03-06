using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.UnitTests.Tests
{
    public class CatalogServiceTest
    {
        private readonly ICatalogService _catalogService;

        private readonly Mock<IItemRepository> _itemRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>> _baseServiceLogger;

        private readonly Item _testItem = new()
        {
            Id = 1,
            Name = "bebeb",
            Description = "more bebebe",
            Size = "xs",
            Price = 500,
            AvailableStock = 50,
            Brand = "not given",
            Category = "bebeb",
            PictureFileName = "1.png"
        };
        public CatalogServiceTest()
        {
            _itemRepository = new Mock<IItemRepository>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();
            _baseServiceLogger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new CatalogService(_dbContextWrapper.Object, _baseServiceLogger.Object, _itemRepository.Object, _logger.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetItemsAsync_Success()
        {
        }

        [Fact]
        public async Task GetItemsAsync_Failed()
        {
        }

        [Fact]
        public async Task GetItemAsync_Success()
        {
            // Arrange
            var id = _testItem.Id;

            _itemRepository.Setup(s => s.GetItemById(id)).ReturnsAsync(_testItem);
            _mapper.Setup(s => s.Map<ItemDto>(_testItem)).Returns(new ItemDto { Id = _testItem.Id });

            // Act
            var result = await _catalogService.GetItemAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_testItem.Id, result.Id);
        }

        [Fact]
        public async Task GetItemAsync_Failed()
        {
            // Arrange
            var id = _testItem.Id;

            _itemRepository.Setup(s => s.GetItemById(id)).ReturnsAsync((Item)null);

            // Act
            var result = await _catalogService.GetItemAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBrandsAsyn_Success()
        {
            // Arrange
            var brandList = new List<string> { _testItem.Brand };

            _itemRepository.Setup(s => s.GetBrandsAsync()).ReturnsAsync(brandList);

            // Act
            var result = await _catalogService.GetBrandsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(_testItem.Brand, result.First());
        }

        [Fact]
        public async Task GetBrandsAsyn_Failed()
        {
            // Arrange
            _itemRepository.Setup(s => s.GetBrandsAsync()).ReturnsAsync((List<string>)null);

            // Act
            var result = await _catalogService.GetBrandsAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCategoriesAsync_Success()
        {
            // Arrange
            var categories = new List<string>() { "Category A", "Category B", "Category C" };
            _itemRepository.Setup(repo => repo.GetCategoriesAsync()).ReturnsAsync(categories);

            // Act
            var result = await _catalogService.GetCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categories, result);
            _itemRepository.Verify(repo => repo.GetCategoriesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCategoriesAsync_Failed()
        {
            // Arrange
            _itemRepository.Setup(repo => repo.GetCategoriesAsync()).ThrowsAsync(new Exception("Something went wrong."));

            // Act
            var result = await _catalogService.GetCategoriesAsync();

            // Assert
            Assert.Null(result);
            _itemRepository.Verify(repo => repo.GetCategoriesAsync(), Times.Once);
        }
    }
}
