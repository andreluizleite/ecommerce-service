
namespace Ecommerce.Services.Domain.Repositories
{
    public class InMemoryCartRepository : ICartRepository
    {
        private readonly Dictionary<Guid, Entities.Cart> _carts = new Dictionary<Guid, Entities.Cart>();

        public Entities.Cart GetCart(Guid cartId)
        {
           _carts.TryGetValue(cartId, out var cart);
            return cart ?? new Entities.Cart(cartId);
        }

        public void Save(Entities.Cart cart)
        {
            _carts[cart.Id] = cart;
        }
    }
}
