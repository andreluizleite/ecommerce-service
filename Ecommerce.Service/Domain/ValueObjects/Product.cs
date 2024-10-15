namespace Ecommerce.Services.Domain.ValueObjects
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; }
        public Money Price { get; }
        public Product(Guid id, string name, Money price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
