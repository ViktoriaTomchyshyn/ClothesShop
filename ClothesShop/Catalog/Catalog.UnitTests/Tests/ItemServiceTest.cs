using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Catalog.UnitTests.Tests
{
    public class ItemServiceTest
    {
        private readonly IItemService _itemService;
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<ItemService>> _logger;
        private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>> _baseServiceLogger;

        private readonly Item _testItem = new Item()
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

        public ItemServiceTest()
        {
            _itemRepository = new Mock<IItemRepository>();

            _mapper = new Mock<IMapper>();

            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            _logger = new Mock<ILogger<ItemService>>();

            _baseServiceLogger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

            _itemService = new ItemService(_dbContextWrapper.Object, _baseServiceLogger.Object, _itemRepository.Object, _logger.Object);
        }

        [Fact]
        public async Task AddItem_Success()
        {
            //Arrange
            var dto = new ItemDto
            {
                Name = _testItem.Name,
                Description = _testItem.Description,
                Category = _testItem.Category,
                Brand = _testItem.Brand,
                Size = _testItem.Size,
                Price = _testItem.Price,
                PictureUrl = _testItem.PictureFileName,
                AvailableStock = _testItem.AvailableStock
            };
            _itemRepository.Setup(x => x.Add(dto.Name, dto.Description, dto.Category, dto.Brand, dto.Size, dto.Price, dto.PictureUrl, dto.AvailableStock)).ReturnsAsync(_testItem.Id);

            //Act
            var result = await _itemService.Add(dto.Name, dto.Description, dto.Category, dto.Brand, dto.Size, dto.Price, dto.PictureUrl, dto.AvailableStock);

            //Assert
            Assert.Equal(_testItem.Id, result);
        }

        [Fact]
        public async Task AddItem_Failed()
        {
            //Arrange
            var dto = new ItemDto
            {
                Name = _testItem.Name,
                Description = _testItem.Description,
                Category = _testItem.Category,
                Brand = _testItem.Brand,
                Size = _testItem.Size,
                Price = _testItem.Price,
                PictureUrl = _testItem.PictureFileName,
                AvailableStock = _testItem.AvailableStock
            };
            _itemRepository.Setup(x => x.Add(dto.Name, dto.Description, dto.Category, dto.Brand, dto.Size, dto.Price, dto.PictureUrl, dto.AvailableStock)).ReturnsAsync(-1);

            //Act
            var result = await _itemService.Add(dto.Name, dto.Description, dto.Category, dto.Brand, dto.Size, dto.Price, dto.PictureUrl, dto.AvailableStock);

            //Assert
            Assert.NotEqual(_testItem.Id, result);
        }

        [Fact]
        public async Task DeleteItem_Success()
        {
            // Arrange
            _itemRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(1);

            // Act
            var result = await _itemService.Delete(_testItem.Id);

            // Assert
            Assert.Equal(_testItem.Id, result);

        }

        [Fact]
        public async Task DeleteItem_Failed()
        {
            // Arrange
            _itemRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync((int?)null);

            // Act
            var result = await _itemService.Delete(_testItem.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateItem_Success()
        {
            // Arrange
            _itemRepository.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(1);

            // Act
            var result = await _itemService.Update(_testItem.Id, "newName", "newDescription", "newCategory", "newBrand", "newSize", 100, "newPictureFileName", 10);

            // Assert
            Assert.Equal(_testItem.Id, result);
        }

        [Fact]
        public async Task UpdateItem_Failed()
        {
            // Arrange
            _itemRepository.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((int?)null);

            // Act
            var result = await _itemService.Update(_testItem.Id, "newName", "newDescription", "newCategory", "newBrand", "newSize", 100, "newPictureFileName", 10);

            // Assert
            Assert.Null(result);
        }

    }
}