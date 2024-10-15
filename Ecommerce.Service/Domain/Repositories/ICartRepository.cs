using Ecommerce.Services.Domain.Entities;
namespace Ecommerce.Services.Domain.Repositories
{
    public interface ICartRepository
    {
        Cart GetCart(Guid cartId);
        void Save(Cart cart);
    }
}
