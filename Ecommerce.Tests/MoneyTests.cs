using Ecommerce.Services.Domain.ValueObjects;

namespace Ecommerce.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void AddingTwoMoneyObjectWithSameCurrency_ShoudReturnCorrectSum()
        {
            //Arrange
            var money1 = new Money(100.00m, "USD");
            var money2 = new Money(50.00m, "USD");

            //Act
            var result = money1 + money2;

            //Assert
            Assert.Equal(150.00m, result.Amount);
            Assert.Equal("USD", result.Currency);
        }
        [Fact]
        public void AddingTwoMoneyObjectsWithDifferentCurrency_ShouldThrowInvalidOperationException()
        {
            //Arrange
            var money1 = new Money(100.00m, "USD");
            var money2 = new Money(50.00m, "EUR");

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => money1 + money2);

            //Assert
            Assert.Equal("Currency mismatch", exception.Message);
        }
    }
}