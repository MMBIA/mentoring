using DataAccess.DTO;

namespace DataAccess
{
    public interface ICartsRepository
    {
        Cart GetCart(int cartId);
        void AddCart(Cart cart);
        List<Item> GetCartItems(int cartId);
        void AddItemToCart(int cartId, Item item);
        void RemoveItemFromCart(int cartId, int itemId);
    }
}