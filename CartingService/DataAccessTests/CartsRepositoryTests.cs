using DataAccess.DTO;
using DataAccess;
using LiteDB;
using Moq;

namespace DataAccessTests
{
    [TestFixture]
    public class CartsRepositoryTests
    {
        private LiteDatabase _database;
        private CartsRepository _cartsRepository;

        [SetUp]
        public void Setup()
        {
            _database = new LiteDatabase(new MemoryStream());
            _cartsRepository = new CartsRepository(_database);
        }

        [TearDown]
        public void TearDown()
        {
            _database.Dispose();
        }

        [Test]
        public void GetCart_ReturnsCart()
        {
            var expectedCart = new Cart { Id = 1 };
            _cartsRepository.AddCart(expectedCart);

            var result = _cartsRepository.GetCart(1);

            Assert.AreEqual(expectedCart.Id, result.Id);
        }

        [Test]
        public void AddCart_InsertsCart()
        {
            var cart = new Cart { Id = 1 };

            _cartsRepository.AddCart(cart);

            var result = _cartsRepository.GetCart(1);

            Assert.AreEqual(cart.Id, result.Id);
        }

        [Test]
        public void GetCartItems_ReturnsCartItems()
        {
            var cart = new Cart
            {
                Id = 1,
                Items = new List<Item>
                {
                    new Item { Id = 1, Name = "Item1" },
                    new Item { Id = 2, Name = "Item2" }
                }
            };
            _cartsRepository.AddCart(cart);

            var result = _cartsRepository.GetCartItems(1);

            Assert.AreEqual(cart.Items.Count, result.Count);

            var expectedItems = cart.Items.OrderBy(x => x.Id).ToList();
            var actualItems = result.OrderBy(x => x.Id).ToList();

            for (int i = 0; i < expectedItems.Count; i++)
            {
                Assert.AreEqual(expectedItems[i].Id, actualItems[i].Id);
                Assert.AreEqual(expectedItems[i].Name, actualItems[i].Name);
            }
        }

        [Test]
        public void AddItemToCart_AddsItem()
        {
            var cart = new Cart { Id = 1, Items = new List<Item>() };
            _cartsRepository.AddCart(cart);

            var newItem = new Item { Id = 1, Name = "NewItem" };
            _cartsRepository.AddItemToCart(1, newItem);

            var updatedCart = _cartsRepository.GetCart(1);
            Assert.AreEqual(1, updatedCart.Items.Count);
            Assert.AreEqual(newItem.Id, updatedCart.Items.First().Id);
            Assert.AreEqual(newItem.Name, updatedCart.Items.First().Name);
        }

        [Test]
        public void RemoveItemFromCart_RemovesItem()
        {
            var itemToRemove = new Item { Id = 1, Name = "Item1" };
            var cart = new Cart
            {
                Id = 1,
                Items = new List<Item> { itemToRemove }
            };
            _cartsRepository.AddCart(cart);

            _cartsRepository.RemoveItemFromCart(1, 1);

            var updatedCart = _cartsRepository.GetCart(1);
            Assert.AreEqual(0, updatedCart.Items.Count);
        }
    }
}