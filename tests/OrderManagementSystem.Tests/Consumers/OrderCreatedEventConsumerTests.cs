using MassTransit;
using Moq;
using OrderManagementSystem.Application.Orders.Events.Consumers;
using OrderManagementSystem.Application.Orders.Events.Models;
using OrderManagementSystem.Domain.Entitites;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Repositories;
using OrderManagementSystem.Domain.Services;

namespace OrderManagementSystem.Tests.Consumers;

public class OrderCreatedEventConsumerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly OrderCreatedEventConsumer _consumer;

    public OrderCreatedEventConsumerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _consumer = new OrderCreatedEventConsumer(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task Consume_ShouldUpdateOrder()
    {
        // Arrange
        var @event = new OrderCreatedEvent
        {
            OrderId = Guid.NewGuid(),
            ProductName = "Test Product",
            Price = 100,
            Status = OrderStatus.Pending
        };

        var contextMock = new Mock<ConsumeContext<OrderCreatedEvent>>();
        contextMock.Setup(c => c.Message).Returns(@event);

        _orderRepositoryMock
            .Setup(repo => repo.UpdateOrderAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);

        // Act
        await _consumer.Consume(contextMock.Object);

        // Assert
        _orderRepositoryMock.Verify(repo =>
            repo.UpdateOrderAsync(It.Is<Order>(o =>
                o.Id == @event.OrderId &&
                o.ProductName == @event.ProductName &&
                o.Status == OrderStatus.Processed
            )),
            Times.Once);
    }
}
