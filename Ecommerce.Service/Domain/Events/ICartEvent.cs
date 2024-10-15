namespace Ecommerce.Services.Domain.Events
{
    public class ICartEvent
    {
        Guid CartId { get; }
        DateTime OccurredOn { get; }
    }
    public class CartCreatedEvent : ICartEvent
    {
        public Guid CartId { get; }
        public DateTime OccurredOn { get; }
        public CartCreatedEvent(Guid cartId)
        {
            CartId = cartId;
            OccurredOn = DateTime.UtcNow;
        }
    }
    public class ItemAddedToCartEvent : ICartEvent
    {
        public Guid CartId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }
        public DateTime OccurredOn { get;}
        public ItemAddedToCartEvent(Guid cartId, Guid productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            OccurredOn = DateTime.UtcNow;
        }
    }
    public class ItemQuantityUpdatedEvent : ICartEvent
    {
        public Guid CartId { get; }
        public Guid ProductId { get; }
        public int NewQuantity { get; }
        public DateTime OccurredOn { get; }
        public ItemQuantityUpdatedEvent(Guid cartId, Guid productId, int newQuantity)
        {
            CartId = cartId;
            ProductId = productId;
            NewQuantity = newQuantity;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
