
using Ecommerce.Services.Domain.Events;
using Ecommerce.Services.Domain.ValueObjects;

namespace Ecommerce.Services.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; }
        private readonly List<CartItem> _items = new List<CartItem>();
        private readonly List<ICartEvent> _eventHistory = new List<ICartEvent>();

        public Cart(Guid id) 
        {
            Id = id;
            RaiseEvent(new CartCreatedEvent(Id));
        }
        public void AddItem(Product product, int quantity)
        {
            var existingItem = _items.FirstOrDefault(item => item.Product.Id == product.Id);
            if(existingItem != null) 
            {
                existingItem.IncriseQuantity(quantity);
                RaiseEvent(new ItemQuantityUpdatedEvent(Id, product.Id, existingItem.Quantity));
            }
            else 
            {
                var newItem = new CartItem(product, quantity);
                _items.Add(newItem);
                RaiseEvent(new ItemAddedToCartEvent(Id, product.Id, newItem.Quantity));
            }
        }
        public Money GetTotalPrice()
        {
            if (!_items.Any())
            {
                return new Money(0m, "USD");
            }

            return _items.Aggregate(new Money(0, _items.First().Product.Price.Currency), (total, item) => total + item.GetTotalPrice());
        }
        public IReadOnlyList<CartItem> GetItems() => _items.AsReadOnly();
        public IReadOnlyList<ICartEvent> GetEventHistory() => _eventHistory.AsReadOnly();
        public void RaiseEvent(ICartEvent carEvent)
        { 
            _eventHistory.Add(carEvent);
        }
        public void ClearEventHistory()
        {
            _eventHistory.Clear();
        }
        public static Cart RebuildFromEvents(IEnumerable<ICartEvent> events)
        {
            Cart cart = null;

            foreach(var cartEvent in events) 
            {
                switch(cartEvent)
                {
                    case CartCreatedEvent createdEvent:
                        cart = new Cart(createdEvent.CartId);
                        break;
                    case ItemAddedToCartEvent itemAddedToCartEvent when cart != null:
                        var product = new Product(itemAddedToCartEvent.ProductId, "Rebuit Product", new Money(0m, "UDS"));
                        cart.AddItem(product, itemAddedToCartEvent.Quantity);
                        break;
                    case ItemQuantityUpdatedEvent itemUpdatedEvent when cart != null:
                        var existingItem = cart.GetItems().FirstOrDefault(item => item.Product.Id == itemUpdatedEvent.ProductId);
                        if(existingItem != null) 
                        {
                            existingItem.SetQuantity(itemUpdatedEvent.NewQuantity);
                        }
                        break;
                }
            }
            return cart;
        }
    }
}
