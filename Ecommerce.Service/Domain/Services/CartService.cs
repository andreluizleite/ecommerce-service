using Ecommerce.Services.Domain.Repositories;
using Ecommerce.Services.Domain.ValueObjects;

namespace Ecommerce.Services.Domain.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository != null ? cartRepository : throw new NullReferenceException(nameof(cartRepository));
        }
        public void AddProductToCart(Guid productId, Product product, int quantity)
        {
            var cart = _cartRepository.GetCart(productId);
            cart.AddItem(product, quantity);
            _cartRepository.Save(cart);
        }
        public Money GetTotalPrice(Guid cardId)
        {
            var cart = _cartRepository.GetCart(cardId);
            return cart.GetTotalPrice();
        }
    }
}
