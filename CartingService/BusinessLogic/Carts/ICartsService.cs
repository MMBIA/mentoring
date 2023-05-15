using BusinessLogic.Models;

namespace BusinessLogic.Carts
{
    public interface ICartsService
    {
        Cart GetCart(int cartId);
        void AddCart(Cart cart);
        IEnumerable<Item> GetCartItems(int cartId);
        void AddItemToCart(int cartId, Item item);
        void RemoveItemFromCart(int cartId, int itemId);
    }
}