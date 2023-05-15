using System.Collections.Generic;
using DataAccess;
using DataAccess.DTO;
using BusinessLogic.Carts;
using BusinessLogic.Models;
using Moq;
using NUnit.Framework;
using AutoMapper;

namespace BusinessLogic.Tests.Carts
{
    [TestFixture]
    public class CartsServiceTests
    {
        private Mock<ICartsRepository> _cartsRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CartsService _cartsService;

        [SetUp]
        public void SetUp()
        {
            _cartsRepositoryMock = new Mock<ICartsRepository>();
            _mapperMock = new Mock<IMapper>();
            _cartsService = new CartsService(_cartsRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public void GetCart_ReturnsCorrectCart()
        {
            // Arrange
            var cartId = 1;
            var cartDto = new DataAccess.DTO.Cart { Id = cartId };
            var cart = new Models.Cart { Id = cartId };
            _cartsRepositoryMock.Setup(x => x.GetCart(cartId)).Returns(cartDto);
            _mapperMock.Setup(x => x.Map<Models.Cart>(cartDto)).Returns(cart);

            // Act
            var result = _cartsService.GetCart(cartId);

            // Assert
            Assert.AreEqual(cart, result);
            _cartsRepositoryMock.Verify(x => x.GetCart(cartId), Times.Once);
            _mapperMock.Verify(x => x.Map<Models.Cart>(cartDto), Times.Once);
        }

        [Test]
        public void AddCart_AddsCartSuccessfully()
        {
            // Arrange
            var cart = new Models.Cart();
            var cartDto = new DataAccess.DTO.Cart();
            _mapperMock.Setup(x => x.Map<DataAccess.DTO.Cart>(cart)).Returns(cartDto);

            // Act
            _cartsService.AddCart(cart);

            // Assert
            _cartsRepositoryMock.Verify(x => x.AddCart(cartDto), Times.Once);
        }

        [Test]
        public void GetCartItems_ReturnsCorrectItems()
        {
            // Arrange
            var cartId = 1;
            var itemsDto = new List<DataAccess.DTO.Item> { new DataAccess.DTO.Item { Id = 1 }, new DataAccess.DTO.Item { Id = 2 } };
            var items = new List<Models.Item> { new Models.Item { Id = 1 }, new Models.Item { Id = 2 } };
            _cartsRepositoryMock.Setup(x => x.GetCartItems(cartId)).Returns(itemsDto);
            _mapperMock.Setup(x => x.Map<IEnumerable<Models.Item>>(itemsDto)).Returns(items);

            // Act
            var result = _cartsService.GetCartItems(cartId);

            // Assert
            Assert.AreEqual(items, result);
            _cartsRepositoryMock.Verify(x => x.GetCartItems(cartId), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<Models.Item>>(itemsDto), Times.Once);
        }

        [Test]
        public void AddItemToCart_AddsItemSuccessfully()
        {
            // Arrange
            var cartId = 1;
            var item = new Models.Item();
            var itemDto = new DataAccess.DTO.Item();
            _mapperMock.Setup(x => x.Map<DataAccess.DTO.Item>(item)).Returns(itemDto);

            // Act
            _cartsService.AddItemToCart(cartId, item);

            // Assert
            _cartsRepositoryMock.Verify(x => x.AddItemToCart(cartId, itemDto), Times.Once);
        }

        [Test]
        public void RemoveItemFromCart_RemovesItemSuccessfully()
        {
            // Arrange
            var cartId = 1;
            var itemId = 1;

            // Act
            _cartsService.RemoveItemFromCart(cartId, itemId);

            // Assert
            _cartsRepositoryMock.Verify(x => x.RemoveItemFromCart(cartId, itemId), Times.Once);
        }
    }
    }