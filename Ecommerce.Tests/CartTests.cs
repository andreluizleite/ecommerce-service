using Ecommerce.Services.Domain.Entities;
using Ecommerce.Services.Domain.Events;
using Ecommerce.Services.Domain.ValueObjects;
using System;
using System.Collections.ObjectModel;

namespace Ecommerce.Tests
{
    public class CartTests
    {
        [Fact]
        public void AddItem_ShouldAddNewItem_WhenProductIsNotInCart()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test Product", new Money(10.00m, "USD"));

            // Act
            cart.AddItem(product, 2);

            // Assert
            Assert.Single(cart.GetItems());
            Assert.Equal(2, cart.GetItems().First().Quantity);
        }

        [Fact]
        public void AddItem_ShouldIncreaseQuantity_WhenProductIsAlreadyInCart()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test Product", new Money(10.00m, "USD"));
            cart.AddItem(product, 1);

            // Act
            cart.AddItem(product, 3);

            // Assert
            Assert.Single(cart.GetItems());
            Assert.Equal(4, cart.GetItems().First().Quantity);
        }

        [Fact]
        public void GetTotalPrice_ShouldReturnCorrectSumOfAllItems()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());
            var product1 = new Product(Guid.NewGuid(), "Product 1", new Money(10.00m, "USD"));
            var product2 = new Product(Guid.NewGuid(), "Product 2", new Money(15.00m, "USD"));
            cart.AddItem(product1, 2); // 20 USD
            cart.AddItem(product2, 1); // 15 USD

            // Act
            var totalPrice = cart.GetTotalPrice();

            // Assert
            Assert.Equal(35.00m, totalPrice.Amount);
            Assert.Equal("USD", totalPrice.Currency);
        }

        [Fact]
        public void GetTotalPrice_ShouldReturnZero_WhenCartIsEmpty()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());

            // Act
            var totalPrice = cart.GetTotalPrice();

            // Assert
            Assert.Equal(0m, totalPrice.Amount);
        }

        [Fact]
        public void GetItems_ShouldReturnReadOnlyListOfItems()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test Product", new Money(10.00m, "USD"));
            cart.AddItem(product, 2);

            // Act
            var items = cart.GetItems();

            // Assert
            Assert.IsType<ReadOnlyCollection<CartItem>>(items);
            Assert.Single(items);
        }
        [Fact]
        public void AddItem_ShoudRaiseCartUpdateEvent_WhenNewItemIsAdded()
        {
            //Arrange
            var cart = new Cart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test Product", new Money(10.00m, "USD"));

            //Act
            cart.AddItem(product, 1);

            //Assert
            var events = cart.GetEventHistory();
            Assert.Single(events);

            var cartUpdatedEvent = events.First() as ItemAddedToCartEvent;
            Assert.NotNull(cartUpdatedEvent);
            Assert.Equal(cart.Id, cartUpdatedEvent.CartId);
        }
        [Fact]
        public void AddItem_ShoudRaiseCartUpdatedEvent_WhereQuantityIncresed()
        {
            //Arrange
            var cart= new Cart(Guid.NewGuid());
            var product = new Product(Guid.NewGuid(), "Test Product", new Money(10.00m, "USD"));
            cart.AddItem(product, 1);

            //Act
            cart.AddItem(product, 2);

            //Assert
            var events = cart.GetEventHistory();
            Assert.Equal(2, events.Count);
            var cartUpdatedEvent = events.Last() as ItemQuantityUpdatedEvent;
            Assert.NotNull(cartUpdatedEvent);
            Assert.Equal(cart.Id, cartUpdatedEvent.CartId);
        }

        [Fact]
        public void RebuildFromEvent_ShoudRestoreCartState_FromEventHistory()
        {
            //Arrange
            var cartId = Guid.NewGuid();
            var events = new List<ICartEvent>
            {
                new CartCreatedEvent(cartId),
                new ItemAddedToCartEvent(cartId, Guid.NewGuid(), 2),
                new ItemQuantityUpdatedEvent(cartId, Guid.NewGuid(), 3),
            };

            //Act
            var rebuiltCar = Cart.RebuildFromEvents(events);

            //Assert
            Assert.Equal(cartId, rebuiltCar.Id);
            Assert.Equal(1, rebuiltCar.GetItems().Count);
            Assert.Equal(3, rebuiltCar.GetItems().First().Quantity);
        }

    }
}
