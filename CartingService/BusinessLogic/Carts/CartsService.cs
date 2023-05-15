using AutoMapper;
using DataAccess;

namespace BusinessLogic.Carts
{
    public class CartsService : ICartsService
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IMapper _mapper;

        public CartsService(ICartsRepository cartsRepository, IMapper mapper)
        {
            _cartsRepository = cartsRepository;
            _mapper = mapper;
        }

        public BusinessLogic.Models.Cart GetCart(int cartId)
        {
            var databaseCart = _cartsRepository.GetCart(cartId);
            var cart = _mapper.Map<BusinessLogic.Models.Cart>(databaseCart);
            return cart;
        }

        public void AddCart(BusinessLogic.Models.Cart cart)
        {
            _cartsRepository.AddCart(_mapper.Map<DataAccess.DTO.Cart>(cart));
        }

        public IEnumerable<BusinessLogic.Models.Item> GetCartItems(int cartId)
        {
            var databaseItems = _cartsRepository.GetCartItems(cartId);
            var items = _mapper.Map<IEnumerable<BusinessLogic.Models.Item>>(databaseItems);
            return items;
        }

        public void AddItemToCart(int cartId, BusinessLogic.Models.Item item)
        {
            _cartsRepository.AddItemToCart(cartId, _mapper.Map<DataAccess.DTO.Item>(item));
        }

        public void RemoveItemFromCart(int cartId, int itemId)
        {
            _cartsRepository.RemoveItemFromCart(cartId, itemId);
        }
    }
}