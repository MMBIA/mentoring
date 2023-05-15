using DataAccess.DTO;
using LiteDB;
using System.Configuration;

namespace DataAccess
{
    public class CartsRepository : ICartsRepository
    {
        private readonly ILiteDatabase _database;
        private bool _disposed;

        public CartsRepository(ILiteDatabase database)
        {
            _database = database;
        }

        public CartsRepository()
        {
            _database = new LiteDatabase(ConfigurationManager.AppSettings["database"]);
        }

        public Cart GetCart(int cartId)
        {
            var carts = _database.GetCollection<Cart>("carts");
            return carts.FindById(cartId);
        }

        public void AddCart(Cart cart)
        {
            var carts = _database.GetCollection<Cart>("carts");
            carts.Insert(cart);
        }

        public List<Item> GetCartItems(int cartId)
        {
            var carts = _database.GetCollection<Cart>("carts");
            var cart = carts.FindById(cartId);
            return cart?.Items;
        }

        public void AddItemToCart(int cartId, Item item)
        {
            var carts = _database.GetCollection<Cart>("carts");
            var cart = carts.FindById(cartId);

            if (cart == null)
            {
                cart = new Cart { Id = cartId };
                carts.Insert(cart);
            }

            cart.Items.Add(item);
            carts.Update(cart);
        }

        public void RemoveItemFromCart(int cartId, int itemId)
        {
            var carts = _database.GetCollection<Cart>("carts");
            var cart = carts.FindById(cartId);

            var itemToRemove = cart?.Items.FirstOrDefault(item => item.Id == itemId);

            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                carts.Update(cart);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _database.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
