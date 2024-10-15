using Ecommerce.Services.Domain.Events;

namespace Ecommerce.Services.Domain.EventHandlers
{
    public class CartUpdatedEventHandler
    {
        public void Handle(CartUpdatedEvent cartUpdatedEvent)
        {
            Console.WriteLine($"Cart {cartUpdatedEvent.Id} was updated. Total Items: {cartUpdatedEvent.UpdateItems.Count}");
        }
    }
}
