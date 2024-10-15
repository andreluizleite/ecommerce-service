using Ecommerce.Services.Domain.Entities;

namespace Ecommerce.Services.Domain.Events
{
    public class CartUpdatedEvent
    {
        public Guid Id { get; }
        public IReadOnlyList<CartItem> UpdateItems{ get; }

        public CartUpdatedEvent(Guid carId, IReadOnlyList<CartItem> updateItems)
        {
            Id = carId;
            UpdateItems = updateItems;
        }
    }
}
