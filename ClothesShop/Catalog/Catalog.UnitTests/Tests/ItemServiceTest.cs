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
        public void GetAll_Success()
        {
        }

        [Fact]
        public void GetAll_Failed()
        {
        }

        [Fact]
        public async Task AddItem_Success()
        {
        }

        [Fact]
        public async Task AddItem_Failed()
        {
        }

        [Fact]
        public async Task DeleteItem_Success()
        {
        }

        [Fact]
        public async Task DeleteItem_Failed()
        {
        }

        [Fact]
        public async Task UpdateItem_Success()
        {
        }

        [Fact]
        public async Task UpdateItem_Failed()
        {
        }

    }
}