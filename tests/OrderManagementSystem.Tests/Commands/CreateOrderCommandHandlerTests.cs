using FluentAssertions;
using Moq;
using OrderManagementSystem.Application.Orders.Commands;
using OrderManagementSystem.Domain.Entitites;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Repositories;
using OrderManagementSystem.Domain.Services;

namespace OrderManagementSystem.Tests.Commands;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IEventBusService> _eventBusMock;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _eventBusMock = new Mock<IEventBusService>();
        _handler = new CreateOrderCommandHandler(
            _orderRepositoryMock.Object,
            _eventBusMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldCreateOrderAndPublishEvent()
    {
        // Arrange
        var command = new CreateOrderCommand("Test Product", 100);
        _orderRepositoryMock
            .Setup(repo => repo.CreateOrderAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);
        _eventBusMock
            .Setup(bus => bus.PublishAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty(); // Order ID boş olmamalı

        _orderRepositoryMock.Verify(
            repo => repo.CreateOrderAsync(It.Is<Order>(o =>
                o.ProductName == "Test Product" &&
                o.Price == 100 &&
                o.Status == OrderStatus.Pending
            )),
            Times.Once
        );

        _eventBusMock.Verify(
            bus => bus.PublishAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}