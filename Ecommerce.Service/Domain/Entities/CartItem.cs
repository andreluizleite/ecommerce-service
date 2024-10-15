using Ecommerce.Services.Domain.ValueObjects;

namespace Ecommerce.Services.Domain.Entities
{
    public class CartItem
    {
        public Product Product { get; }
        public int Quantity { get; private set; }
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public void IncriseQuantity(int quantity)
        {
            Quantity = quantity;
        }
        public Money GetTotalPrice() 
        {
            return new Money(Product.Price.Amount * Quantity, Product.Price.Currency);
        }

        public void SetQuantity(int newQuantity)
        {
            if(newQuantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }
            Quantity = newQuantity;
        }
    }
}
