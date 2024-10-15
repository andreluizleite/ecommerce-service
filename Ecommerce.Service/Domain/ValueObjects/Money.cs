namespace Ecommerce.Services.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; }
        public string Currency { get; }
        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public static Money operator +(Money a, Money b) 
        {
            if(a.Currency != b.Currency) 
            {
                throw new InvalidOperationException("Currency mismatch");
            }
            return new Money(a.Amount + b.Amount, a.Currency);
        }
    }
}
